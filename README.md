# ArgSum

ArgSum is a command-line tool for updating the checksums of Microprose F1GP (and GP2)
related files such as name files, setups and tracks.

For these types of files, the final four bytes of file data contains a checksum. If the checksum is
incorrect, F1GP will refuse to load the files.


## Usage

Basic usage simply consists of passing the file that you want to update as a parameter to the tool, e.g.

`ArgSum.exe file-to-update.dat`

If ArgSum does not recognize the type of file you will be asked to confirm updating the checksum.
This is to avoid accidentally destroying other types of files.

If you want to force updating of the file despite ArgSum not recognizing it,
pass `--force` (or `-f`), e.g.

`ArgSum.exe file-to-update.dat --force`

If you use ArgSum as a Send To-tool in Windows you may want to pass
the `--pause` (or `-p`) parameter to make it wait for you to hit any key before
closing the command line window.

Pass the `--help` parameter (or no parameters at all) to print simple
usage documentation (basically the text above).


## Download

Download the latest version from the "Releases" tab here on GitHub.


## Acknowledgements

* Paul Hoad - for the C++ code for calculating F1GP file checksums he sent me in 1998
