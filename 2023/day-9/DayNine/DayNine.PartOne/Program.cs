// See https://aka.ms/new-console-template for more information
using DayNine.Core;

Console.WriteLine("Running solution for Day 9, part 1...");

// get file contents
//var input = File.ReadAllLines("../../input-9-demo.txt");
var input = File.ReadAllLines("../../input-9.txt");

// collect the sequences
var sequenceCollections = new List<DerivedSequenceCollection>();
foreach (string line in input)
{
    var values = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(value => int.Parse(value));
    var sequence = new Sequence(values);
    sequenceCollections.Add(new(sequence));
}

Console.WriteLine($"Found {sequenceCollections.Count} sequences.");

// calculate the derived sequences for each sequence
sequenceCollections.ForEach(item => item.CalculateDerivedSequences());

// extrapolate the sequences
int answer = sequenceCollections.Select(item => item.ExtrapolateSequences()).Sum();

Console.WriteLine($"Answer is:  {answer}");

/*
Running solution for Day 9, part 1...
Found 200 sequences.
Extrapolated value:  924
Extrapolated value:  -1467203
Extrapolated value:  18257656
Extrapolated value:  1518173
Extrapolated value:  -12
Extrapolated value:  -369856
Extrapolated value:  2852245
Extrapolated value:  1766
Extrapolated value:  7318293
Extrapolated value:  35554625
Extrapolated value:  -471
Extrapolated value:  16333836
Extrapolated value:  203
Extrapolated value:  1819
Extrapolated value:  1741725
Extrapolated value:  5594821
Extrapolated value:  -93
Extrapolated value:  245079
Extrapolated value:  7471794
Extrapolated value:  22261570
Extrapolated value:  24923776
Extrapolated value:  22565754
Extrapolated value:  78065
Extrapolated value:  1360770
Extrapolated value:  26635785
Extrapolated value:  3304259
Extrapolated value:  28271718
Extrapolated value:  21332635
Extrapolated value:  2173200
Extrapolated value:  2690940
Extrapolated value:  9291415
Extrapolated value:  3620249
Extrapolated value:  9485794
Extrapolated value:  425356
Extrapolated value:  1499702
Extrapolated value:  317
Extrapolated value:  26196
Extrapolated value:  13052083
Extrapolated value:  24967689
Extrapolated value:  361692
Extrapolated value:  733843
Extrapolated value:  27935526
Extrapolated value:  28243051
Extrapolated value:  5530896
Extrapolated value:  166876
Extrapolated value:  16144041
Extrapolated value:  28233910
Extrapolated value:  -131415
Extrapolated value:  18145683
Extrapolated value:  367589
Extrapolated value:  2017596
Extrapolated value:  22943045
Extrapolated value:  17706239
Extrapolated value:  63287
Extrapolated value:  128684
Extrapolated value:  381450
Extrapolated value:  18729996
Extrapolated value:  17747528
Extrapolated value:  2191
Extrapolated value:  6213718
Extrapolated value:  4777122
Extrapolated value:  14703994
Extrapolated value:  -7494
Extrapolated value:  -3740782
Extrapolated value:  13584
Extrapolated value:  42
Extrapolated value:  222019
Extrapolated value:  16338823
Extrapolated value:  -54590
Extrapolated value:  5734512
Extrapolated value:  6192
Extrapolated value:  21434412
Extrapolated value:  29133034
Extrapolated value:  20494276
Extrapolated value:  43329
Extrapolated value:  20224
Extrapolated value:  3219403
Extrapolated value:  18563330
Extrapolated value:  831643
Extrapolated value:  22114544
Extrapolated value:  18520471
Extrapolated value:  20921232
Extrapolated value:  15578477
Extrapolated value:  239
Extrapolated value:  94269
Extrapolated value:  76980
Extrapolated value:  62345
Extrapolated value:  -151318
Extrapolated value:  25771288
Extrapolated value:  40378665
Extrapolated value:  24369261
Extrapolated value:  14993312
Extrapolated value:  31224773
Extrapolated value:  30076
Extrapolated value:  17109440
Extrapolated value:  3108113
Extrapolated value:  17857550
Extrapolated value:  1243923
Extrapolated value:  13269209
Extrapolated value:  26713556
Extrapolated value:  4443606
Extrapolated value:  245126
Extrapolated value:  11475047
Extrapolated value:  13915
Extrapolated value:  5930519
Extrapolated value:  1041268
Extrapolated value:  -2614740
Extrapolated value:  7738308
Extrapolated value:  19791048
Extrapolated value:  244
Extrapolated value:  106958
Extrapolated value:  19873092
Extrapolated value:  -124958
Extrapolated value:  21004540
Extrapolated value:  3323310
Extrapolated value:  11389386
Extrapolated value:  13233954
Extrapolated value:  22241901
Extrapolated value:  13946138
Extrapolated value:  9382862
Extrapolated value:  12447477
Extrapolated value:  758271
Extrapolated value:  19604139
Extrapolated value:  10542
Extrapolated value:  180175
Extrapolated value:  19856960
Extrapolated value:  24586448
Extrapolated value:  16655195
Extrapolated value:  274274
Extrapolated value:  5017120
Extrapolated value:  18363698
Extrapolated value:  7768695
Extrapolated value:  37514844
Extrapolated value:  22119084
Extrapolated value:  23626255
Extrapolated value:  23333974
Extrapolated value:  5096064
Extrapolated value:  158
Extrapolated value:  -9295
Extrapolated value:  12886677
Extrapolated value:  528518
Extrapolated value:  17308737
Extrapolated value:  17797573
Extrapolated value:  946085
Extrapolated value:  1145550
Extrapolated value:  2052062
Extrapolated value:  17257172
Extrapolated value:  11768395
Extrapolated value:  17604263
Extrapolated value:  412324
Extrapolated value:  12176902
Extrapolated value:  21796706
Extrapolated value:  1499865
Extrapolated value:  9416407
Extrapolated value:  35651060
Extrapolated value:  8707258
Extrapolated value:  2099
Extrapolated value:  38576
Extrapolated value:  59217
Extrapolated value:  5179940
Extrapolated value:  178282
Extrapolated value:  32572332
Extrapolated value:  73577
Extrapolated value:  1702
Extrapolated value:  21913
Extrapolated value:  17325966
Extrapolated value:  -93336
Extrapolated value:  16549390
Extrapolated value:  22825
Extrapolated value:  160
Extrapolated value:  4375898
Extrapolated value:  19614793
Extrapolated value:  8321891
Extrapolated value:  -14148
Extrapolated value:  -1023131
Extrapolated value:  16676604
Extrapolated value:  15242
Extrapolated value:  28574787
Extrapolated value:  4465259
Extrapolated value:  2908452
Extrapolated value:  2407
Extrapolated value:  23533163
Extrapolated value:  13998190
Extrapolated value:  132
Extrapolated value:  8124150
Extrapolated value:  34581477
Extrapolated value:  13717134
Extrapolated value:  -542862
Extrapolated value:  9468074
Extrapolated value:  18492006
Extrapolated value:  21543447
Extrapolated value:  9356842
Extrapolated value:  22728577
Extrapolated value:  -1085012
Extrapolated value:  15804147
Extrapolated value:  10122002
Extrapolated value:  37482713
Extrapolated value:  13204055
Extrapolated value:  10669503
Extrapolated value:  17291803
Answer is:  1972648895
*/