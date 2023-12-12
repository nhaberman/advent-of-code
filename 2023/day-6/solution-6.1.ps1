#$RaceTimes = @(7, 15, 30)
$RaceTimes = @(34, 90, 89,  86)
#$RecordDistances = @(9,  40, 200)
$RecordDistances = @(204, 1713, 1210, 1780)

$PossibleWinners = @(0) * $RaceTimes.Length

# analyze each race
for ($raceNumber = 0; $raceNumber -lt $RaceTimes.Count; $raceNumber++) {
    "Analyzing possibilities for race #$($raceNumber + 1)..."

    # test every charge time option to see if it exceeds the record time
    for ($chargeTime = 0; $chargeTime -lt $RaceTimes[$raceNumber]; $chargeTime++) {
        $TotalDistance = $RaceTimes[$raceNumber] * $chargeTime - $chargeTime * $chargeTime
        
        if ($TotalDistance -gt $RecordDistances[$raceNumber]) {
            $PossibleWinners[$raceNumber]++
        }
    }
    "Found $($PossibleWinners[$raceNumber]) possible record-breakers."
}

$Answer = 1
foreach ($item in $PossibleWinners) {
    $Answer *= $item
}

"Answer is:  " + $Answer

# Answers:
# 633080 -> CORRECT