namespace ProtocoloAgil.Base
{
  public  class  GetConfig
    {
        public static string Config()
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings["ProtocoloAgilConnectionString"].ConnectionString;
        }


        public static string Key()
        {
            return "!#!@23?Fa";
        }

      public static int Escola()
      {
          return 1;
      }
    }
}
