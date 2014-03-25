using System;
using System.IO;
using System.Text.RegularExpressions;

namespace EasyAccess.Infrastructure.Util.T4
{
    public class T4TempDll
    {

        public string DllFullName { get; private set; }
        public string TempDllFullName { get; private set; }

        public T4TempDll(string solutionPath, string modelMoudle)
        {
            DllFullName = Path.Combine(solutionPath, modelMoudle + @"\bin\Release\" + modelMoudle + ".dll");
#if DEBUG
            DllFullName = Path.Combine(solutionPath, modelMoudle + @"\bin\Debug\" + modelMoudle + ".dll");
#endif

            var tempDllPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\";
            foreach (var tempDll in new DirectoryInfo(tempDllPath).GetFiles())
            {
                try
                {
                    if (new Regex(modelMoudle + "\\.\\d{8}\\.dll$").IsMatch(tempDll.FullName))
                    {
                        File.Delete(tempDll.FullName);
                    }
                }
                catch (Exception ex)
                {
                    continue;;
                }
            }
                
            TempDllFullName = tempDllPath + modelMoudle + "." + DateTime.Now.ToString("ddhhmmss") + ".dll";
            File.Copy(DllFullName, TempDllFullName);
        }
    }
}