namespace InOutLib
{
    public class CsvHelper : FileHelper
    {
        #region private attributs
        private string _fileName;
        private string _path;
        #endregion private attributs

        #region constructor
        public CsvHelper(string path, string fileName, char separator = ';') : base(path, fileName)
        {
            _path = path;
            _fileName = fileName;

            if (!IsCharSupported(separator))
            {
                throw new UnsupportedSeparatorException();
            }
            else
            {
                _fullPath = path + @"//" + fileName;
            }
        }
        #endregion constructor

        #region public methods 
        public new void ExtractFileContent()
        {
            //TODO ExtractFileContent - 6pts

            StreamReader streamReader = new StreamReader(_fullPath);
            string line;

            // Reads and stores lines from the file until eof.
            while ((line = streamReader.ReadLine()) != null)
            {
                this.Lines.Add(line);
            }
            streamReader.Close();

            if (this.Lines.Count == 0)
            {
                throw new EmptyFileException();
            }

            foreach (string registeredLine in Lines)
            {
                string lineToTest = registeredLine.ToString();
                //Si la partie en commentaire est retirée, le test 'NominalCase_GetFileContent ne passe plus'
                //Cette partie devrait faire passer le dernier test, mais à la place, elle bloque l'autre.
                if (lineToTest == "" /*|| lineToTest.Contains(";;")*/)
                {
                    throw new StructureException();
                }
            }
        }
        #endregion public methods

        #region private methods
        private bool IsCharSupported(char separator)
        {
            //TODO IsCharSupported - 2pts
            if (separator != ';')
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion privates methods

        #region nested classes
        public class CsvHelperException : FileHelperException{}
        public class UnsupportedSeparatorException : CsvHelperException { }
        public class StructureException : CsvHelperException { }
        #endregion nested classes
    }
}
