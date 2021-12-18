using System;
using System.IO;

namespace ProtocoloAgil.Base
{
    /// <summary>
    /// Summary description for FileManipulator
    /// </summary>
    public class FileManager
    {
        private readonly string path;
        public FileManager(String path)
        {
            this.path = path;
        }

        public void Escreve(String linha)
        {
            using (var wr = new StreamWriter(path, true))
            {
                wr.WriteLine(linha);
                wr.Close();
            }
        }

        public string Leitura()
        {
            var linha = "";
            using (var rd = new StreamReader(path))
            {
                {
                    while (!rd.EndOfStream)
                    {
                        linha += rd.ReadLine();
                    }
                    rd.Close();
                }
                return linha;
            }
        }
    }
}