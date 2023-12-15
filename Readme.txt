Hello, Its Me Nena Beaner, i just wanted to say hi.
preview:

https://github-production-user-asset-6210df.s3.amazonaws.com/127537866/290936088-82ef51ce-7bc1-4424-9bb9-9c9755113406.png
Version 1.02:
--
-
I made a few security updates in the form. Changing the size of the window is no longer possible.

Also, I added a login method to the Launcher, which checks the user's authenticity and if the login is successful, the Launcher's Play button is activated.

these settings can always be changed in the source code, see CreatorsGUI.cs at the end there is a Login_Click method and it has a connectionstring that contains the sql connection user data to the mssql server.

for now, this version is not necessarily intended for beginners, it requires a little knowledge.
-
alternative mirror
Source Download: https://file.io/FGGK4mGWfBDN
--
(CreatorsGUI.cs is not viewable as Designer Mode, because at the begin of CreatorsGUI.cs

namespace CreatorsGUI {
/*
public static class AppData {
public static CreatorsGUI CreatorsGUIForm { get; set; }

}*/
you must first comment out this after this you are able to view it normally, remember to uncomment it after you are done. )

Database Patches(this checks the UGradeID via database):

+ go into mssql open tables, go where Login.Dbo, Right click it and select Design Table, and add new Field of Value "UGradeID" with datatype Int. and Save it. and then goto Stored Programmables, and Modify Insert Account, and add new value to where you see "Insert INTO LOGIN UserID / Password , add UGradeID to this and execute this, and you are done with the sets.

    this doesnt include additonal values, you are just re-using the existing data in database, and re-using this in login table.


To update the Existing Database login table after the patch, just open query window in mssql in your database, and write this sql query and execute, it will change all users ugradeid value into 0, the default normal user.

Manually Update all the users in login table:

    UPDATE Login
    SET ugradeid = 0;

