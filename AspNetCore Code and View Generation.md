# ASP.NET Core Code and View generation from the command line

## Introduction

## What do I need

``` xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
  </PropertyGroup>
  <ItemGroup>
    <PackageReference Include="Microsoft.AspNetCore.All" Version="2.0.3" />
    <PackageReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Design" Version="2.0.1" />
  </ItemGroup>
  <ItemGroup>
    <DotNetCliToolReference Include="Microsoft.VisualStudio.Web.CodeGeneration.Tools" Version="2.0.1" />
  </ItemGroup>
</Project>
```

To generate a view use

``` cli
dotnet aspnet-codegenerator view Edit Edit -m WebSite.ViewModels.ToDo.EditViewModel -udl -outDir Views/ToDo
```

