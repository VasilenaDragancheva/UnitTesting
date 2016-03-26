namespace BoatRacingSimulator.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// An interface representing the class that carries information common for each race
    /// </summary>
    /// 
    public interface IRace
    {
        /// <summary>
        /// Distance wich each partipicpant should pass
        /// </summary>
        int Distance { get; }

        /// <summary>
        /// Int,Speed of wind during the race,it affects boats` speed
        /// </summary>
        int WindSpeed { get; }

        /// <summary>
        /// Int,Speed of ocean durint current race,it affects boats` speed
        /// </summary>
        int OceanCurrentSpeed { get; }

        /// <summary>
        /// Boolean, true if as participants are allows boats with boat engines,false if are not allowed
        /// </summary>
        bool AllowsMotorboats { get; }

        /// <summary>
        /// Adding participant to current race;
        /// </summary>
        /// <param name="boat"> object which is implemeting interface IBoat</param>
        void AddParticipant(IBoat boat);

        /// <summary>
        /// List of all participants that take place in current race
        /// </summary>
        /// <returns></returns>
        IList<IBoat> GetParticipants();
    }
}