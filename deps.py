import os
import json
import shutil
import requests

from io import BytesIO
from os import path
from zipfile import ZipFile

DIR = path.join(path.dirname(__file__), "deps")

DEPS = [
    "MaxWasUnavailable/Virality",
    "loaforc/Flashcard",
]

deps = [f"https://thunderstore.io/api/experimental/package/{it}/" for it in DEPS]
urls = []

for item in deps:
    urls.append(json.loads(requests.get(item).text)["latest"]["download_url"])

for url in urls:
    print(f"Downloading: {url}")

    data = requests.get(url).content
    io = BytesIO(data)

    with ZipFile(io, "r") as zip:
        for file in zip.filelist:
            if file.orig_filename.endswith(".dll"):
                zip.extract(file, DIR)

                os.rename(
                    path.join(DIR, file.orig_filename),
                    path.join(DIR, path.basename(file.orig_filename)),
                )

                shutil.rmtree(path.join(DIR, file.orig_filename.split("/")[0]))
