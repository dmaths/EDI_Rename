EDI_Rename
==========

File Rename For TMW EDI by Folder

Quickly put together to rename files in specific folders.

Used with a Windows scheduled task to run on a regular basis to:
1) Test if there are files in the directories
2) If there are files, have they already been renamed.  If yes, skip, if no, rename them
3) Load any files that are over an hour old into a list
4) If the list has anything in it, send an email.


In order to run this, you must update the DefaultApp.config file to the correct SMTP settings.
Also must add the System.Configuration Reference in VS2012.  See: http://stackoverflow.com/questions/1274852/the-name-configurationmanager-does-not-exist-in-the-current-context
