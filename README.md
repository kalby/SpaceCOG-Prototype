SpaceCOG-Prototype
==================

DECO3800 SpaceCOG Unity Prototype Codebase

To remove all changes to your cache:

git reset
git checkout .
git clean -fdx

To do a full push and pull:

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