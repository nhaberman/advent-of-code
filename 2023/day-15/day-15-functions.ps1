function Invoke-HashAlgorithm ($InputChar, [ref]$CurrentIntValue) {

    if ($CurrentIntValue.Value.GetType() -ne [int]) {
        "Current value $CurrentIntValue is not an int, the algorithm cannot be applied."
    }

    # get the ASCII code for the character
    $CharCode = [byte][char]$InputChar

    # increase the current value by the ASCII code value
    $CurrentIntValue.Value += $CharCode

    # multiply the current value by 17
    $CurrentIntValue.Value *= 17

    # divide the current value by 256 and set the remainder as the current value
    $CurrentIntValue.Value %= 256
}

function Invoke-HashAlgorithmOnString ($InputString) {
    [int]$Result = 0

    foreach ($char in $InputString.ToCharArray()) {
        Invoke-HashAlgorithm -CurrentIntValue ([ref]$Result) -InputChar $char
    }

    return $Result
}