import os
import json
import shutil
import zipfile

from os import path
from xml.etree.cElementTree import ElementTree as ET

# ======================= Config =======================

out_dir = "./build"
tmp_dir = "./.tmp"
out_path = "./bin/Release/netstandard2.1/ConfigurableWarning.dll"
csproj_path = "./ConfigurableWarning.csproj"
url = "https://github.com/RedstoneWizard08/ConfigurableWarning"

deps = [
    "RugbugRedfern-MyceliumNetworking-1.0.12",
    "CommanderCat101-ContentSettings-1.2.2",
]

files = [
    "README.md",
    "icon.png",
    "LICENSE",
]

# ======================= Magic! =======================

csproj = ET().parse(csproj_path)

mod_ver = csproj.findall("PropertyGroup")[0].findall("Version")[0].text
mod_name = csproj.findall("PropertyGroup")[0].findall("AssemblyName")[0].text
mod_desc = csproj.findall("PropertyGroup")[0].findall("Description")[0].text

if not path.exists(tmp_dir):
    os.makedirs(tmp_dir)

if not path.exists(out_dir):
    os.makedirs(out_dir)
    
shutil.copyfile(out_path, path.join(tmp_dir, path.basename(out_path)))

for file in files:
    shutil.copyfile(file, path.join(tmp_dir, path.basename(file)))

manifest = {
    "name": mod_name,
    "version_number": mod_ver,
    "description": mod_desc,
    "website_url": url,
    "dependencies": deps,
}

with open(path.join(tmp_dir, "manifest.json"), "w") as f:
    f.write(json.dumps(manifest, indent=4))
    
if path.exists(f"{mod_name}-{mod_ver}.zip"):
    os.remove(f"{mod_name}-{mod_ver}.zip")

with zipfile.ZipFile(path.join(out_dir, f"{mod_name}-{mod_ver}.zip"), "w") as z:
    for root, _, files in os.walk(tmp_dir):
        for file in files:
            z.write(path.join(root, file), path.relpath(path.join(root, file), tmp_dir))

shutil.rmtree(tmp_dir)

print("Packaging complete!")
