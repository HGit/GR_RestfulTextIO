@echo off

REM
REM
REM
REM	COLUMNS: LastName, FirstName, Gender, DOB, FavoriteColor
REM

REM TextIOConsole.exe "--header-index" "-1" "--record-delimiter" "\r\n" "--field-delimiter" "\s" "--file" ".\files\space_delimited.txt" "--order-by" "Gender ASC"
REM TextIOConsole.exe "--header-index" "-1" "--record-delimiter" "\r\n" "--field-delimiter" "\t" "--file" ".\files\tab_delimited.txt" "--order-by" "Gender ASC"
REM TextIOConsole.exe "--header-index" "-1" "--record-delimiter" "\r\n" "--field-delimiter" "," "--file" ".\files\comma_delimited.txt" "--order-by" "Gender ASC"
REM TextIOConsole.exe "--header-index" "-1" "--record-delimiter" "\r\n" "--field-delimiter" "|" "--file" ".\files\pipe_delimited.txt" "--order-by" "Gender ASC"
REM
REM
REM 
REM HEADER INDEX = 0
REM
REM SORT:
REM	"Gender ASC"
REM	"LastName ASC"
REM	"DOB ASC"
REM	"LastName DESC"
REM
REM	"Gender ASC, LastName DESC"	* Custom/Personal





REM TextIOConsole.exe "--header-index" "0" "--record-delimiter" "\r\n" "--field-delimiter" "\s" "--file" ".\files\space_delimited.txt" "--order-by" "Gender ASC"
REM TextIOConsole.exe "--header-index" "0" "--record-delimiter" "\r\n" "--field-delimiter" "\t" "--file" ".\files\tab_delimited.txt" "--order-by" "Gender ASC"
REM TextIOConsole.exe "--header-index" "0" "--record-delimiter" "\r\n" "--field-delimiter" "," "--file" ".\files\comma_delimited.txt" "--order-by" "Gender ASC"
REM TextIOConsole.exe "--header-index" "0" "--record-delimiter" "\r\n" "--field-delimiter" "|" "--file" ".\files\pipe_delimited.txt" "--order-by" "Gender ASC"

TextIOConsole.exe "--header-index" "0" "--record-delimiter" "\r\n" "--field-delimiter" "|" "--file" ".\files\pipe_delimited.txt" "--order-by" "Gender ASC, LastName DESC"



pause