using System;
using System.Collections.Generic;
using Gtk;

namespace Grsyncng
{
    public class MainClass
    {
        public static MainWindow mainWindow;
        public static CommandGenerator commandGenerator = new CommandGenerator();

        static Dictionary<Argument,CommandArgument> commandArguments = new Dictionary<Argument, CommandArgument>
            {
                { Argument.Destination, new StringArgument() },
                { Argument.Source, new StringArgument() },
                { Argument.Verbose, new SwitchArgument("-v", "increase verbosity") },
                { Argument.Quiet, new SwitchArgument("-q", "suppress non-error messages") },
                { Argument.NoMotd, new SwitchArgument("--no-motd", "suppress daemon-mode MOTD (see caveat)") },
                { Argument.SkipUseChecksum, new SwitchArgument("-c", "skip based on checksum, not mod-time & size") },
                { Argument.Archive, new SwitchArgument("-a", "archive mode; equals -rlptgoD (no -H, -A, -X)") },
                { Argument.Recursive, new SwitchArgument("-r", "recurse into directories") },
                { Argument.RelativePaths, new SwitchArgument("-R", "use relative paths") },
                { Argument.NoImpliedDirs, new SwitchArgument("--no-implied-dirs", "don't send implied dirs with --relative") },
                { Argument.MakeBackups, new SwitchArgument("-b", "make backups (see --suffix & --backup-dir)") },
                { Argument.SkipReceiverNewerFiles, new SwitchArgument("-u", "skip files that are newer on the receiver") },
                { Argument.Inplace, new SwitchArgument("--inplace", "update destination files in-place") },
                { Argument.Append, new SwitchArgument("--append", "append data onto shorter files") },
                { Argument.AppendVerify, new SwitchArgument("--append-verify", "--append w/old data in file checksum") },
                { Argument.NotRecursive, new SwitchArgument("-d", "transfer directories without recursing") },
                { Argument.Symlinks, new SwitchArgument("-l", "copy symlinks as symlinks") },
                { Argument.TransformSymlinkFileOrDir, new SwitchArgument("-L", "transform symlink into referent file/dir") },
                { Argument.CopyUnsafeLinks, new SwitchArgument("--copy-unsafe-links", "only \"unsafe\" symlinks are transformed") },
                { Argument.SafeLinks, new SwitchArgument("--safe-links", "ignore symlinks that point outside of the tree") },
                { Argument.TransformSymlinkDir, new SwitchArgument("-k", "transform symlink to dir into referent dir") },
                { Argument.SymlinkedReceiverDirAsDir, new SwitchArgument("-K", "treat symlinked dir on receiver as dir") },
                { Argument.PreserveHardLinks, new SwitchArgument("-H", "preserve hard links") },
                { Argument.PreservePermissions, new SwitchArgument("-p", "preverse permissions") },
                { Argument.PreserveExecutability, new SwitchArgument("-E", "preserve executability") },
                { Argument.PreserveACL, new SwitchArgument("-a", "preserve ACLs (implies -p") },
                { Argument.PreserveExtendedAttributes, new SwitchArgument("-X", "preserve extended attributes") },
                { Argument.PreserveOwner, new SwitchArgument("-o", "preserve owner (super-user only)") },
                { Argument.PreserveGroup, new SwitchArgument("-g", "preserve group") },
                { Argument.Devices, new SwitchArgument("--devices", "preserve device files (super-user only)") },
                { Argument.Specials, new SwitchArgument("--specials", "preserve special files") },
                { Argument.BigD, new SwitchArgument("-D", "same as --devices --specials") },
                { Argument.PreserveModTime, new SwitchArgument("-t", "preserve modification times") },
                { Argument.OmitDirectoriesFromTimes, new SwitchArgument("-O", "omit directories from --times") },
                { Argument.Super, new SwitchArgument("--super", "receiver attempts super-user activities") },
                { Argument.FakeSuper, new SwitchArgument("--fake-super", "store/recover privileged super-user activities") },
                { Argument.HandleSparseFilesEfficiently, new SwitchArgument("-S", "handle sparse files efficiently") },
                { Argument.Simulate, new SwitchArgument("-n", "perform a trial run with no changes made") },
                { Argument.CopyFilesWhole, new SwitchArgument("-W", "copy files whole (w/o delta-xfer algorithm)") },
                { Argument.DontCrossFSBoundaries, new SwitchArgument("-x", "don't cross filesystem boundaries") },
                { Argument.Existing, new SwitchArgument("--existing", "skip creating new files on receiver") },
                { Argument.IgnoreExisting, new SwitchArgument("--ignore-existing", "skip updating files that exist on receiver") },
                { Argument.RemoveSourceFiles, new SwitchArgument("--remove-source-files", "sender removes synchronized files (non-dir)") },
                { Argument.Delete, new SwitchArgument("--delete", "delete extraneous files from dest dirs") },
                { Argument.DeleteBefore, new SwitchArgument("--delete-before", "receiver deletes before transfer (default)") },
                { Argument.DeleteDuring, new SwitchArgument("--delete-during", "receiver deletes during xfer, not before") },
                { Argument.DeleteDelay, new SwitchArgument("--delete-delay", "find deletions during, delete after") },
                { Argument.DeleteAfter, new SwitchArgument("--delete-after", "receiver deletes after transfer, not before") },
                { Argument.DeleteExcluded, new SwitchArgument("--delete-excluded", "also delete excluded files from dest dirs") },
                { Argument.IgnoreErrors, new SwitchArgument("--ignore-errors", "delete event if there are I/O errors") },
                { Argument.Force, new SwitchArgument("--force", "force deletion of dirs even if not empty") },
                { Argument.Partial, new SwitchArgument("--partial", "keep partially transferred files") },
                { Argument.DelayUpdates, new SwitchArgument("--delay-updates", "put all updated files into place at end") },
                { Argument.PruneEmptyDirs, new SwitchArgument("-m", "prune empty directory chains from file-list") },
                { Argument.NumericsIds, new SwitchArgument("--numeric-ids", "don't map uid/gid values by user/group name") },
                { Argument.DontSkip, new SwitchArgument("-I", "don't skip files that match size and time") },
                { Argument.SizeOnly, new SwitchArgument("--size-only", "skip files that match in size") },
                { Argument.FindSimilarFile, new SwitchArgument("-y", "find similar file for basis if no dest file") },
                { Argument.Compress, new SwitchArgument("-z", "compress data during file transfer") },
                { Argument.IgnoreCVS, new SwitchArgument("-C", "auto-ignore files in the same way CVS does") },
                { Argument.BigF, new SwitchArgument("-F", "same as --filter='dir-merge /.rsync-filters'") },
                { Argument.DelimitedZero, new SwitchArgument("-0", "all *from/filter files are delimited by 0s") },
                { Argument.NoSpaceSplitting, new SwitchArgument("-s", "no space-splitting; wildcard chars only") },
                { Argument.BlockingIO, new SwitchArgument("--blocking-io", "use blocking I/O for the remote shell") },
                { Argument.Stats, new SwitchArgument("--stats", "give some file-transfer stats") },
                { Argument.EightBit, new SwitchArgument("-8", "leave high-bit chars unescaped in output") },
                { Argument.HumanReadable, new SwitchArgument("-h", "output numbers in a human-readable format") },
                { Argument.Progress, new SwitchArgument("--progress", "show progress during transfer") },
                { Argument.SameAsPartialProgress, new SwitchArgument("-P", "same as --partial --progress") },
                { Argument.ChangeSummary, new SwitchArgument("-i", "output a change-summary for all updates") },
                { Argument.ListOnly, new SwitchArgument("--list-only", "list the files instead of copying them") },
                { Argument.PreferIPv4, new SwitchArgument("-4", "prefer IPv4") },
                { Argument.PreferIPv6, new SwitchArgument("-6", "prefer IPv6") },
            };

        public static void Main(string[] args)
        {
            RegisterEventListeners();

            Application.Init();
            mainWindow = new MainWindow();

            mainWindow.LoadCommandArguments(commandArguments);
            UpdateCommand();
            mainWindow.Show();
            
            Application.Run();
        }


        public static void UpdateCommand()
        {
            string command = commandGenerator.GenerateCommand(commandArguments);
            mainWindow.SetCommand(command);
        }


        protected static void RegisterEventListeners()
        {
            // update command on every argument change
            foreach (KeyValuePair<Argument, CommandArgument> kvp in commandArguments)
            {
                kvp.Value.OnValueChanged += (object sender) =>
                {
                    UpdateCommand();
                };
            }

            RegisterExcludeGroup(Argument.Archive, new Argument[]{
                Argument.Recursive,
                Argument.Symlinks,
                Argument.PreservePermissions,
                Argument.PreserveOwner,
                Argument.PreserveGroup,
                Argument.PreserveModTime,
                Argument.BigD,
            });

            RegisterExcludeGroup(Argument.SameAsPartialProgress, new Argument[]{
                Argument.Partial,
                Argument.Progress,
            });

            RegisterExclude(Argument.PreferIPv4, Argument.PreferIPv6);
        }

        protected static void RegisterExclude(Argument a, Argument b)
        {
            RegisterExcludeGroup(a, new Argument[] { b });
            RegisterExcludeGroup(b, new Argument[] { a });
        }

        protected static void RegisterExcludeGroup(Argument master, Argument[] slaves)
        {
            GetArgument(master).OnValueChanged += (object sender) =>
            {
                bool value = (bool)((CommandArgument)sender).Value;
                foreach (Argument slave in slaves)
                {
                    CommandArgument slaveArg = GetArgument(slave);
                    //slave.Value = !value;
                    slaveArg.Interactable = !value;
                }
            };
        }

        protected static CommandArgument GetArgument(Argument argument)
        {
            return commandArguments[argument];
        }
    }

}
