# NbExePackage

## Why Do This?

I need to share code with others but not want include some files(e.g.: version control files or bin debug files)

## How to use?

Package Source Folder With Exclude configs(use .config file can override init settings)

    InitExcludes = {
        ".vshost.exe",
        ".vshost.exe.config",
        ".vshost.exe.manifest",
        ".pdb",
        ".user",
        ".suo",
        ".vssscc",
        ".vspscc",
        "\\bin",
        "\\obj",
        "\\packages"
    };

------------------
e.g. execute NbExePackage.exe in NbExePackage Folder, output:

    添加： App.config
    添加： NbExePackage.csproj
    添加： PackageHelper.cs
    添加： Program.cs
    添加： readme.md
    添加： Properties\AssemblyInfo.cs
    共计添加打包文件：6个


------------------