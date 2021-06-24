﻿#define COMMAND_NAME_UPPER

#if DEBUG
#undef SPAWNTO_x86
#undef SPAWNTO_X64
#define SPAWNTO_X86
#define SPAWNTO_X64
#endif

using Apollo.CommandModules;
using Apollo.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Apollo.Evasion
{
    internal static class EvasionManager
    {
        private static string SpawnTo64 = "C:\\Windows\\System32\\rundll32.exe";
        private static string SpawnTo64Args = "";
        private static string SpawnTo86 = "C:\\Windows\\SysWOW64\\rundll32.exe";
        private static string SpawnTo86Args = "";

        internal struct SacrificialProcessStartupInformation
        {
            internal string Application;
            internal string Arguments;
        }

        internal static SacrificialProcessStartupInformation GetSacrificialProcessStartupInformation()
        {
            SacrificialProcessStartupInformation results = new SacrificialProcessStartupInformation();
            if (IntPtr.Size == 8)
            {
                results.Application = SpawnTo64;
                results.Arguments = SpawnTo64Args;
            }
            else
            {
                results.Application = SpawnTo86;
                results.Arguments = SpawnTo86;
            }
            return results;
        }

#if SPAWNTO_X64
        internal static bool SetSpawnTo64(string fileName, string args = "")
        {
            bool bRet = false;
            if (FileUtils.IsExecutable(fileName))
            {
                SpawnTo64 = fileName;
                if (!string.IsNullOrEmpty(args))
                    SpawnTo64Args = args;
                bRet = true;
            }
            return bRet;
        }
#endif
#if SPAWNTO_X86
        internal static bool SetSpawnTo86(string fileName, string args = "")
        {
            bool bRet = false;
            if (FileUtils.IsExecutable(fileName))
            {
                SpawnTo86 = fileName;
                if (!string.IsNullOrEmpty(args))
                    SpawnTo86Args = args;
                bRet = true;
            }
            return bRet;
        }
#endif
    }
}
