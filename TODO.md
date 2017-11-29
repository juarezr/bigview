# Todo List

## Features

* Tools
  - Row/field filtering
  - Hide/show columns
  - Search and replace
  - Export to Microsoft Excel
  - Convert between file formats
  - File editing  
* File Formats planned:
  - [Avro](https://avro.apache.org)
    * SchemaRegistryDotNet
  - Delimited text files, CSV.
  - Fixed length text files
  - [ORC](https://orc.apache.org)
  - Support external schema on Parquet/Avro , etc...
* Reading from cloud:
  - [Amazon S3](https://aws.amazon.com/s3/)
  - [Google Cloud Storage](https://cloud.google.com/storage/)
  - [Microsoft Azure Blob Storage](https://azure.microsoft.com/en-us/services/storage/blobs/)
  - Browse remote/cloud files
* Porting
  - Porting to .net Core 2.1
  - Porting to Linux

## Enhancements

* Recent open files/folders
* Selection mode: multiple, rows, columns
* Row numbers

## Architecture

* Core:
  - Do not load whole file. Allowing reading big files wiht many GB of size.
* Refactoring
  - Split file loading/writing into a new lib
