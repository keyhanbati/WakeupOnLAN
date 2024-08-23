@setlocal enableextensions enabledelayedexpansion
@echo off
rem set addr=%1
set addr=KEYHAN-PC
call WOL cli
:loop
ping -n 1 %addr% | find "TTL"
if not errorlevel 1 set error=up
if errorlevel 1 set error=down
::cls
echo Result: %addr% is %error%
if "%error%"=="down" goto :loop
timeout 5
start mstsc /v:%addr%