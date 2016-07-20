namespace Session7
{
    public class Console : IConsole
    {
        public string Input(string prompt)
        {
            System.Console.WriteLine(prompt);
            return System.Console.ReadLine();
        }

        public void Output(string output)
        {
            System.Console.WriteLine(output);
        }
    }
}
