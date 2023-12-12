function Get-Digit {
    param (
        $DigitWord
    )
    
    switch ($DigitWord) {
        "one" { return 1 }
        "two" { return 2 }
        "three" { return 3 }
        "four" { return 4 }
        "five" { return 5 }
        "six" { return 6 }
        "seven" { return 7 }
        "eight" { return 8 }
        "nine" { return 9 }
    }
}

$InputData = Get-Content -Path ".\day-1\input-1.txt"

$Answer = 0

$InputData | ForEach-Object {
    $InputLine = $_

    # check for a spelled-out first digit
    $FirstDigitMatch = (Select-String -InputObject $InputLine -Pattern "^.*?(?<first>one|two|three|four|five|six|seven|eight|nine).*$")
    if ($FirstDigitMatch.Matches.Success) {
        $NewFirstDigit = Get-Digit -DigitWord $FirstDigitMatch.Matches.Groups[1].Value
        $InputLine = $InputLine.remove($FirstDigitMatch.Matches.Groups[1].Index, $FirstDigitMatch.Matches.Groups[1].Length).insert($FirstDigitMatch.Matches.Groups[1].Index, $NewFirstDigit)
    }

    # check for a spelled-out last digit
    $LastDigitMatch = (Select-String -InputObject $InputLine -Pattern "^.*(?<last>one|two|three|four|five|six|seven|eight|nine).*?$")
    if ($LastDigitMatch.Matches.Success) {
        $NewLastDigit = Get-Digit -DigitWord $LastDigitMatch.Matches.Groups[1].Value
        $InputLine = $InputLine.remove($LastDigitMatch.Matches.Groups[1].Index, $LastDigitMatch.Matches.Groups[1].Length).insert($LastDigitMatch.Matches.Groups[1].Index, $NewLastDigit)
    }

    [int]$FirstDigit = (Select-String -InputObject $InputLine -Pattern "(?<=^\D*)(\d)").matches.groups[0].value
    [int]$LastDigit = (Select-String -InputObject $InputLine -Pattern "(\d)(?=\D*$)").matches.groups[0].value

    $CalibrationValue = $FirstDigit * 10 + $LastDigit

    $Answer += $CalibrationValue
}

"Answer is:  " + $Answer
