:: Script to parse arp output
@ECHO OFF
SETLOCAL ENABLEEXTENSIONS
SET me=[%~n0]: 
IF %1.==. GOTO :NOIP
SET pimac=b8-27-eb
SET arg1=%1

ECHO %me%Using %pimac% as MAC prefix to identify raspis
ECHO %me%Using %arg1% as an identifier for arp -a -N

FOR /f "tokens=1delims= " %%i IN ('arp -a -N %arg1% ^| findstr %pimac%' ) DO ( 
ECHO %%i >> piips.txt
ECHO %me%%%i
)

pause
GOTO :EOF
:NOIP
  ECHO Not enough parameters. This script expects a network address to determine the NIC to search raspis in
  ECHO Usage: %~n0 [network-address]
  pause
  GOTO :EOF