namespace EvolutionTask
{
    class Program
    {


        static void Main(string[] args)
        {
            var sampleFilePath = Path.Combine(Utils.ExecutableBaseFolder, Consts.SAMPLE_FILE_NAME);

            var data = CsvReaderHelper.ReadEmployeeFile(sampleFilePath);

            Console.WriteLine("Hello World!");
        }
    }
}