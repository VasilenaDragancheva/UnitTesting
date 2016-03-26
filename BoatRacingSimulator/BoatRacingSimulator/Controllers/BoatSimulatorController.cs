namespace BoatRacingSimulator.Controllers
{
    using System;
    using System.Linq;
    using System.Text;

    using Database;

    using Exceptions;

    using Interfaces;

    using Models;

    using Utility;

    public class BoatSimulatorController : IBoatSimulatorController
    {
        public BoatSimulatorController(IBoatSimulatorDatabase database, IRace currentRace)
        {
            this.Database = database;
            this.CurrentRace = currentRace;
        }

        public BoatSimulatorController()
            : this(new BoatSimulatorDatabase(), null)
        {
        }

        public IRace CurrentRace { get; private set; }

        public IBoatSimulatorDatabase Database { get; private set; }

        /// <summary>
        /// Creates boat engine if is type supplied and model uniqe
        /// </summary>
        /// <param name="model">string, representing model, shoudl be unique</param>
        /// <param name="horsepower">int, represeting horsepower, needed for calculation of output</param>
        /// <param name="displacement">int, represeting horsepower, needed for calculation of output</param>
        /// <param name="engineType">type of engine, defines constant for calculation of output</param>
        /// <returns>String, if all valid parameters is message for success,else indicating invalid argument</returns>
        public string CreateBoatEngine(string model, int horsepower, int displacement, string engineType)
        {
            IBoatEngine engine;
            switch (engineType)
            {
                case "Jet":
                    engine = new JetEngine(model, horsepower, displacement);
                    break;
                case "Sterndrive":
                    engine = new SterndriveEngine(model, horsepower, displacement);
                    break;
                default:
                    throw new NotImplementedException();
            }

            this.Database.Engines.Add(engine);
            return string.Format(
                "Engine model {0} with {1} HP and displacement {2} cm3 created successfully.", 
                model, 
                horsepower, 
                displacement);
        }

        public string CreatePowerBoat(string model, int weight, string firstEngineModel, string secondEngineModel)
        {
            IBoatEngine firstEngine = this.Database.Engines.GetItem(firstEngineModel);
            IBoatEngine secondEngine = this.Database.Engines.GetItem(secondEngineModel);
            var boat = new PowerBoat(model, weight, firstEngine, secondEngine);
            this.Database.Boats.Add(boat);
            return string.Format("Power boat with model {0} registered successfully.", model);
        }

        public string CreateRowBoat(string model, int weight, int oars)
        {
            var boat = new RowBoat(model, weight, oars);
            this.Database.Boats.Add(boat);
            return string.Format("Row boat with model {0} registered successfully.", model);
        }

        public string CreateSailBoat(string model, int weight, int sailEfficiency)
        {
            var boat = new SailBoat(model, weight, sailEfficiency);

            this.Database.Boats.Add(boat);
            return string.Format("Sail boat with model {0} registered successfully.", model);
        }

        public string CreateYacht(string model, int weight, string engineModel, int cargoWeight)
        {
            IBoatEngine engine = this.Database.Engines.GetItem(engineModel);
            var yacht = new Yacht(model, weight, engine, cargoWeight);

            this.Database.Boats.Add(yacht);
            return string.Format("Yacht with model {0} registered successfully.", model);
        }

        public string GetStatistic()
        {
            var boats = this.CurrentRace.GetParticipants().OrderBy(p => p.GetType().Name).GroupBy(b => b.GetType().Name);
            int totalParticipans = this.CurrentRace.GetParticipants().Count();
            var result = new StringBuilder();
            foreach (var boatType in boats)
            {
                decimal perecentage = (decimal)boatType.Count() / totalParticipans;
                result.AppendLine(string.Format("{0} -> {1:f2}%", boatType.Key, perecentage * 100));
            }

            return result.ToString().Trim();
        }

        public string OpenRace(int distance, int windSpeed, int oceanCurrentSpeed, bool allowsMotorboats)
        {
            IRace race = new Race(distance, windSpeed, oceanCurrentSpeed, allowsMotorboats);
            this.ValidateRaceIsEmpty();
            this.CurrentRace = race;
            return
                string.Format(
                    "A new race with distance {0} meters, wind speed {1} m/s and ocean current speed {2} m/s has been set.", 
                    distance, 
                    windSpeed, 
                    oceanCurrentSpeed);
        }

        /// <summary>
        /// Adding boat as participant in current race
        /// </summary>
        /// <param name="model">String, representing model of boat, should be unique</param>
        /// <returns>String, if is set race and is existing model boat,else indicating invalid argument</returns>
        public string SignUpBoat(string model)
        {
            this.ValidateRaceIsSet();
            IBoat boat = this.Database.Boats.GetItem(model);
            if (!this.CurrentRace.AllowsMotorboats && boat is IMotorBoat)
            {
                throw new ArgumentException(Constants.IncorrectBoatTypeMessage);
            }

            this.CurrentRace.AddParticipant(boat);
            return string.Format("Boat with model {0} has signed up for the current Race.", model);
        }

        public string StartRace()
        {
            // Bottleneck calculating winner maybe faster ans simpler
            this.ValidateRaceIsSet();
            var participants = this.CurrentRace.GetParticipants();
            if (participants.Count < 3)
            {
                throw new InsufficientContestantsException(Constants.InsufficientContestantsMessage);
            }

            var winners =
                this.CurrentRace.GetParticipants()
                    .OrderByDescending(p => p.CalculateRaceSpeed(this.CurrentRace) > 0)
                    .Take(3)
                    .ToList();
            var withNegativeSpeed =
                this.CurrentRace.GetParticipants()
                    .OrderByDescending(p => p.CalculateRaceSpeed(this.CurrentRace) <= 0)
                    .Take(3)
                    .ToList();

            if (winners.Count < 3)
            {
                winners.AddRange(withNegativeSpeed.Take(3 - winners.Count));
            }

            var firstOutput = this.FormatOuput(winners[0]);
            var secondOutput = this.FormatOuput(winners[1]);
            var thirdOutput = this.FormatOuput(winners[2]);

            var result = new StringBuilder();
            result.AppendLine(string.Format("First place: {0}", firstOutput))
                .AppendLine(string.Format("Second place: {0}", secondOutput))
                .Append(string.Format("Third place: {0}", thirdOutput));

            this.CurrentRace = null;

            return result.ToString();
        }

        private object FormatOuput(IBoat boat)
        {
            double timeCalculted = this.CurrentRace.Distance / boat.CalculateRaceSpeed(this.CurrentRace);
            var timeToString = timeCalculted <= 0 ? "Did not finish!" : timeCalculted.ToString("0.00") + " sec";
            string result = string.Format("{0} Model: {1} Time: {2}", boat.GetType().Name, boat.Model, timeToString);
            return result;
        }

        private void ValidateRaceIsSet()
        {
            if (this.CurrentRace == null)
            {
                throw new NoSetRaceException(Constants.NoSetRaceMessage);
            }
        }

        private void ValidateRaceIsEmpty()
        {
            if (this.CurrentRace != null)
            {
                throw new RaceAlreadyExistsException(Constants.RaceAlreadyExistsMessage);
            }
        }
    }
}