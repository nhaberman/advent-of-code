#$RaceTime = 71530
$RaceTime = 34908986
#$RecordDistance = 940200
$RecordDistance = 204171312101780

$PossibleWinners = $RaceTime + 1 # add the "zero" time

# test charge time options until one is a record-breaker
for ($chargeTime = 0; $chargeTime -lt $RaceTime; $chargeTime++) {
    $TotalDistance = $RaceTime * $chargeTime - $chargeTime * $chargeTime
    
    if ($TotalDistance -le $RecordDistance) {
        $PossibleWinners--
    }
    else {
        break;
    }
}

# test charge time options from the end until one is a record-breaker
for ($chargeTime = $RaceTime; $chargeTime -gt 0; $chargeTime--) {
    $TotalDistance = $RaceTime * $chargeTime - $chargeTime * $chargeTime
    
    if ($TotalDistance -le $RecordDistance) {
        $PossibleWinners--
    }
    else {
        break;
    }
}

"Answer is:  " + $PossibleWinners

# Answers:
# 20048742 -> too high (from wolfram alpha)
# 20048741 -> CORRECT