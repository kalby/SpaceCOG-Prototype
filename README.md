SpaceCOG-Prototype
==================

DECO3800 SpaceCOG Unity Prototype Codebase

To remove all changes to your cache, first do a pull-push if you have uncommitted work, see below. Then do:

git reset

git checkout .

git clean -fdx

pull-push:

//For the sake of safety, save your Unity Project, close Unity and wait 5-10 seconds before doing things with git.

//Theoretically it ignores the lockfile and you should be able to leave it open but Unity temp files are annoying to deal with over git.

//Do it at your own risk.


git add --a

git commit -m "Something for the team."

git pull

// Here things might conflict

// if they do and there's not that many files conflicting

git mergetool

// Otherwise seek Dan for help.

// if they don't

git push


Done.