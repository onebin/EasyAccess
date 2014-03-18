using System;
using System.IO;

namespace EasyAccess.Infrastructure.Util.T4
{
    public class T4TempDll
    {

        public string DllFullName { get; private set; }
        public string TempDllFullName { get; private set; }

        public T4TempDll(string solutionPath, string modelMoudle)
        {
            DllFullName = Path.Combine(solutionPath, modelMoudle + @"\bin\Debug\" + modelMoudle + ".dll");
            var tempDllPath = solutionPath + "\\packages\\T4TempDll\\";
            if (File.Exists(DllFullName))
            {
                if (!Directory.Exists(tempDllPath))
                {
                    Directory.CreateDirectory(tempDllPath);
                }
                else
                {
                    foreach (var tempDll in new DirectoryInfo(tempDllPath).GetFiles())
                    {
                        try
                        {
                            File.Delete(tempDll.FullName);
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
            }

            TempDllFullName = tempDllPath + modelMoudle + "." + DateTime.Now.ToString("ddhhmmss") + ".dll";
            File.Copy(DllFullName, TempDllFullName);
        }



    }
}