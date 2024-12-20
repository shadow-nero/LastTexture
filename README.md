# LastTexture

This tool was developed to simplify the conversion of `.SHTXPS` texture files to `.PNG` and vice versa. It's designed for use in modding and working with games or applications that utilize the `.SHTXPS` format.  

Whether you're a modding enthusiast or a developer, **LastTexture** aims to make texture management more accessible and efficient.

---

## Table of Contents

- [Features](#features)
- [Usage](#usage)
- [How It Works](#how-it-works)
- [Batch Conversion](#batch-conversion)
- [Special Thanks](#special-thanks)

---

## Features

- **Convert `.SHTXPS` to `.PNG`:** Easily extract textures from `.SHTXPS` files for editing or analysis.  
- **Convert `.PNG` to `.SHTXPS`:** Repackage your edited textures back into the `.SHTXPS` format.  
- **Bulk Conversion Support:** Process entire directories of files in one command.  
- **Flexible Output Options:** Specify output locations, or default to the input directory.  

---

## Usage

### Commands

To use the tool, download the latest release from the [LastTexture releases page](https://github.com/shadow-nero/LastTexture/releases). Then execute the program from the command line with the following syntax:

```bash
LastTexture.exe [command] [input path] [output path]
```

#### Available Commands

- **`-e`**: Convert `.SHTXPS` to `.PNG`  
- **`-r`**: Convert `.PNG` to `.SHTXPS`  
- **`-help`**: Display usage instructions  

---

### Examples

#### Single File Conversion

1. Convert a `.SHTXPS` file to `.PNG`:
   ```bash
   LastTexture.exe -e example.shtxps example.png
   ```

2. Convert a `.PNG` file to `.SHTXPS`:
   ```bash
   LastTexture.exe -r example.png example.shtxps
   ```

#### Batch Conversion (Folder)

1. Convert all `.SHTXPS` files in a directory to `.PNG`:
   ```bash
   LastTexture.exe -e input_folder output_folder
   ```

2. Convert all `.PNG` files in a directory to `.SHTXPS`:
   ```bash
   LastTexture.exe -r input_folder output_folder
   ```

---

## How It Works

The tool operates by leveraging two core classes:  

- **`SHTXReader`**: Extracts `.SHTXPS` files into `.PNG` format.  
- **`SHTXWritter`**: Repackages `.PNG` files back into `.SHTXPS`.  

Both classes ensure compatibility with the specific structure and requirements of the `.SHTXPS` format.  

When converting files in bulk, the tool iterates through each file in the directory, checking extensions and ensuring proper output naming.

---

## Batch Conversion

The tool can automatically detect files in the specified directory and convert them in bulk. For instance, if you're working with a game's texture folder, this feature allows you to convert all files at once, saving time and effort.

---

## Special Thanks

A heartfelt thanks to the following individuals and resources for their contributions and inspiration:  

- **[AraAra]** First person shows that it is possible to modify the game's textures easily and efficiently 
. 

Feel free to contribute, report issues, or suggest features via the [LastTexture GitHub repository](https://github.com/shadow-nero/LastTexture).  

--- 
