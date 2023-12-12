$InputData = Get-Content -Path ".\day-1\input-1.txt"

$Answer = 0

$InputData | ForEach-Object {
    [int]$FirstDigit = (Select-String -InputObject $_ -Pattern "(?<=^\D*)(\d)").matches.groups[0].value
    [int]$LastDigit = (Select-String -InputObject $_ -Pattern "(\d)(?=\D*$)").matches.groups[0].value

    $CalibrationValue = $FirstDigit * 10 + $LastDigit

    $Answer += $CalibrationValue
}

"Answer is:  " + $Answer