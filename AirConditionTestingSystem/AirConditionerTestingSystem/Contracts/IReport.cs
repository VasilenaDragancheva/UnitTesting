namespace AirConditionerTestingSystem.Contracts
{
    /// <summary>
    /// Declares needed nehavior for every object cretaed when testing aircoditioner
    /// </summary>
    public interface IReport
    {
        /// <summary>
        /// Manifacture of tested conditioner
        /// </summary>
        string Manifacturer { get; set; }

        /// <summary>
        /// Model of tested conditioner
        /// </summary>
        string Model { get; set; }

        /// <summary>
        /// Failing test or not
        /// Value of 1 when is passed test
        /// Value of 0 when is failed
        /// </summary>
        int Mark { get; }
    }
}