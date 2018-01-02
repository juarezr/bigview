# bigview
Desktop app for displaying data from common big data file formats as parquet, avro, csv, etc...

## Features

* Preview records in a grid table
* Preview record details field by field
* Preview and print all/selected records/cells
* Copy to clipboard all/selected records/cells

## File Formats supported

* [Parquet](https://parquet.apache.org/)
* [Avro](https://avro.apache.org) (uncompressed, deflate)

## Status

* Working In Progress...
* Alpha stage
* Should work well for local files
* Windows only 

## Building

* Used Visual Studio 2015+

## Roadmap

* Can you help developing?

### Goals

* Support: preview/inspecting of common file formats used in big data (all-in-one)
* Productivity: Use already existing tecnology, libraries, existing code example (stackoverflow, googling examples)
* Pragmatism

### Unpriorized feature list

* Tools
  - Row/field filtering
  - Search and replace
  - Export to Microsoft Excel
  - Convert between file formats
  - File editing  
* File Formats planned:
  - Delimited text files, CSV.
  - Fixed length text files
  - [ORC](https://orc.apache.org)
  - Support external schema on Parquet/Avro , etc...
  - Support nested types on Parquet/Avro , etc...
* Reading from cloud:
  - [Amazon S3](https://aws.amazon.com/s3/)
  - [Google Cloud Storage](https://cloud.google.com/storage/)
  - [Microsoft Azure Blob Storage](https://azure.microsoft.com/en-us/services/storage/blobs/)
* Porting
  - Porting to .net Core 2.1
  - Porting to Linux
  - Waiting for Microsoft port C# Windows.Forms to .net Core 
    * [Blog Post](https://blogs.msdn.microsoft.com/dotnet/2017/11/16/announcing-the-windows-compatibility-pack-for-net-core/)
    * [Design: Cross platform UX for .NET Core 3.0 ](https://github.com/dotnet/designs/issues/12)
    * [Issue: Support Full System.Drawing Functionality on .NET Core](https://github.com/dotnet/corefx/issues/20325)
    * [Issue: Port Winforms to CoreFX](https://github.com/dotnet/corefx/issues/21803)
    * [Possible alternatives...](https://blog.lextudio.com/the-story-about-net-cross-platform-ui-frameworks-dd4a9433d0ea)
    * [Alternative example](https://github.com/akoeplinger/mono-winforms-netcore)
    * It can take a while...
