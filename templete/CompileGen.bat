rem For code runner
cd templete > nul

del ../Gen.exe
csc Gen.cs /o+ /out:../Gen.exe
