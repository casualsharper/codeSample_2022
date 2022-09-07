namespace EvolutionTask
{
    class Program
    {
        private static string getExecutableBaseFolder =>
            Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)
                ?? throw new ArgumentNullException(nameof(getExecutableBaseFolder));

        static void Main(string[] args)
        {
            var sampleFilePath = Path.Combine(getExecutableBaseFolder, Consts.SAMPLE_FILE_NAME);

            var data = CsvReaderHelper.ReadEmployeeFile(sampleFilePath);

            Console.WriteLine("Hello World!");
        }
    }
}