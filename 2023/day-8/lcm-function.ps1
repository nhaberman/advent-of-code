function Get-LeastCommonMultiple {
    param (
        [int] $FirstNumber,
        [int] $SecondNumber
    )

    # only proceed for positive numbers
    if ($FirstNumber -le 0 -or $SecondNumber -le 0) {
        return -1
    }

    # determine the smaller number
    if ($FirstNumber -lt $SecondNumber) {
        $LowerNumber = $FirstNumber
        $HigherNumber = $SecondNumber
    } else {
        $LowerNumber = $SecondNumber
        $HigherNumber = $FirstNumber
    }

    # N1 * N2 is guaranteed to be a multiple of N1 and N2
    # test up to N2 multiples of N1 to find if any is also a multiple
    # the first one found is the LCM
    for ($i = 1; $i -le $HigherNumber; $i++) {
        $Multiple = $i * $LowerNumber

        if ($Multiple % $HigherNumber -eq 0) {
            # this multiple is the LCM, return it and end
            return $Multiple
        }
    }
}