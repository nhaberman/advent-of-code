$InputData = Get-Content -Path ".\day-3\input-modified.txt"

$Answer = 0

for ($i = 0; $i -lt $InputData.Count; $i++) {
    # if the first or last line, do nothing
    if ($i -eq 0 -or $i -eq $InputData.Count - 1) {
        continue;
    }

    'Checking line ' + $i

    # find any symbols and save their index for the current and surrounding lines
    $PriorLineSymbolIndices = @()
    $CurrentLineSymbolIndices = @()
    $NextLineSymbolIndices = @()
    $PriorLineSymbolIndices += (Select-String -InputObject $InputData[$i - 1] -Pattern "([^\.\d])" -AllMatches).Matches | Select-Object -ExpandProperty Index
    $CurrentLineSymbolIndices += (Select-String -InputObject $InputData[$i] -Pattern "([^\.\d])" -AllMatches).Matches | Select-Object -ExpandProperty Index
    $NextLineSymbolIndices += (Select-String -InputObject $InputData[$i + 1] -Pattern "([^\.\d])" -AllMatches).Matches | Select-Object -ExpandProperty Index
    $SymbolMatchIndices = $PriorLineSymbolIndices + $CurrentLineSymbolIndices + $NextLineSymbolIndices | Sort-Object -Unique

    # get all part numbers from the current line
    $PartNumberMatches = (Select-String -InputObject $InputData[$i] -Pattern "\d+" -AllMatches)
    if ($PartNumberMatches.Matches.Success) {
        # check each part number
        foreach ($PartNumberMatch in $PartNumberMatches.Matches) {
            
            [int]$PartNumber = $PartNumberMatch.value
            $StartingPosition = $PartNumberMatch.Index
            $EndingPosition = $PartNumberMatch.Index + $PartNumberMatch.Length - 1

            'Checking Part Number:  ' + $PartNumber

            # check if the part number is by a symbol
            for ($j = $StartingPosition - 1; $j -le $EndingPosition + 1; $j++) {
                if ($SymbolMatchIndices -contains $j) {
                    'Part number is valid!'
                    $Answer += $PartNumber
                    break;
                }
            }
        }
    }
}

"Answer is:  " + $Answer

# Answers:
# 554694 -> X too low
# 557705 -> CORRECT