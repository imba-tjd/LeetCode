rem For code runner
cd template > nul

del ../Gen.exe
csc Gen.cs /o+ /out:../Gen.exe
