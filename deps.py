import os
import json
import shutil
import requests
import subprocess

from io import BytesIO
from os import path
from zipfile import ZipFile

DIR = path.join(path.dirname(__file__), "deps")

DEPS = [
    "MaxWasUnavailable/Virality",
    "loaforc/Flashcard",
    "OriginalCheese/Spookdivers",
    "Playboi/HellDivers",
]

deps = [f"https://thunderstore.io/api/experimental/package/{it}/" for it in DEPS]
urls = []

for item in deps:
    urls.append(json.loads(requests.get(item).text)["latest"]["download_url"])

if path.exists(DIR):
    shutil.rmtree(DIR)

for url in urls:
    print(f"Downloading: {url}")

    data = requests.get(url).content
    io = BytesIO(data)

    with ZipFile(io, "r") as zip:
        for file in zip.filelist:
            if file.orig_filename.endswith(".dll"):
                zip.extract(file, DIR)

                if "/" in file.orig_filename:
                    os.rename(
                        path.join(DIR, file.orig_filename),
                        path.join(DIR, path.basename(file.orig_filename)),
                    )

                    shutil.rmtree(path.join(DIR, file.orig_filename.split("/")[0]))
                
                file = path.join(DIR, path.basename(file.orig_filename))
                
                subprocess.check_output(["dotnet", "assembly-publicizer", file])
