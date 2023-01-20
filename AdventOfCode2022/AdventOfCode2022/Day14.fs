module AdventOfCode2022_Day14

VerifyTests.VerifierSettings.AddExtraSettings
    (fun settings ->
        settings.NullValueHandling <- Argon.NullValueHandling.Include
        settings.DefaultValueHandling <- Argon.DefaultValueHandling.Include
    )

open NUnit.Framework
open FParsec

let input =
    [|
        "461,87 -> 461,80 -> 461,87 -> 463,87 -> 463,80 -> 463,87 -> 465,87 -> 465,80 -> 465,87 -> 467,87 -> 467,86 -> 467,87"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "460,159 -> 460,154 -> 460,159 -> 462,159 -> 462,151 -> 462,159 -> 464,159 -> 464,152 -> 464,159"
        "464,109 -> 464,111 -> 463,111 -> 463,118 -> 473,118 -> 473,111 -> 468,111 -> 468,109"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "478,33 -> 478,37 -> 471,37 -> 471,41 -> 483,41 -> 483,37 -> 482,37 -> 482,33"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "466,96 -> 470,96"
        "494,22 -> 499,22"
        "499,13 -> 499,16 -> 498,16 -> 498,19 -> 507,19 -> 507,16 -> 501,16 -> 501,13"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "460,145 -> 460,146 -> 462,146"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "460,159 -> 460,154 -> 460,159 -> 462,159 -> 462,151 -> 462,159 -> 464,159 -> 464,152 -> 464,159"
        "454,168 -> 458,168"
        "476,60 -> 476,61 -> 489,61"
        "502,26 -> 507,26"
        "447,106 -> 452,106"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "498,24 -> 503,24"
        "478,33 -> 478,37 -> 471,37 -> 471,41 -> 483,41 -> 483,37 -> 482,37 -> 482,33"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "499,13 -> 499,16 -> 498,16 -> 498,19 -> 507,19 -> 507,16 -> 501,16 -> 501,13"
        "457,166 -> 461,166"
        "478,33 -> 478,37 -> 471,37 -> 471,41 -> 483,41 -> 483,37 -> 482,37 -> 482,33"
        "464,109 -> 464,111 -> 463,111 -> 463,118 -> 473,118 -> 473,111 -> 468,111 -> 468,109"
        "499,13 -> 499,16 -> 498,16 -> 498,19 -> 507,19 -> 507,16 -> 501,16 -> 501,13"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "478,33 -> 478,37 -> 471,37 -> 471,41 -> 483,41 -> 483,37 -> 482,37 -> 482,33"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "457,99 -> 461,99"
        "499,13 -> 499,16 -> 498,16 -> 498,19 -> 507,19 -> 507,16 -> 501,16 -> 501,13"
        "463,162 -> 467,162"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "457,104 -> 462,104"
        "450,104 -> 455,104"
        "476,60 -> 476,61 -> 489,61"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "462,143 -> 473,143 -> 473,142"
        "483,44 -> 483,48 -> 481,48 -> 481,55 -> 489,55 -> 489,48 -> 487,48 -> 487,44"
        "463,99 -> 467,99"
        "461,87 -> 461,80 -> 461,87 -> 463,87 -> 463,80 -> 463,87 -> 465,87 -> 465,80 -> 465,87 -> 467,87 -> 467,86 -> 467,87"
        "478,33 -> 478,37 -> 471,37 -> 471,41 -> 483,41 -> 483,37 -> 482,37 -> 482,33"
        "472,168 -> 476,168"
        "472,123 -> 472,124 -> 476,124"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "483,44 -> 483,48 -> 481,48 -> 481,55 -> 489,55 -> 489,48 -> 487,48 -> 487,44"
        "489,30 -> 494,30"
        "462,143 -> 473,143 -> 473,142"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "461,87 -> 461,80 -> 461,87 -> 463,87 -> 463,80 -> 463,87 -> 465,87 -> 465,80 -> 465,87 -> 467,87 -> 467,86 -> 467,87"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "482,30 -> 487,30"
        "461,87 -> 461,80 -> 461,87 -> 463,87 -> 463,80 -> 463,87 -> 465,87 -> 465,80 -> 465,87 -> 467,87 -> 467,86 -> 467,87"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "460,145 -> 460,146 -> 462,146"
        "460,168 -> 464,168"
        "460,159 -> 460,154 -> 460,159 -> 462,159 -> 462,151 -> 462,159 -> 464,159 -> 464,152 -> 464,159"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "492,28 -> 497,28"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "464,109 -> 464,111 -> 463,111 -> 463,118 -> 473,118 -> 473,111 -> 468,111 -> 468,109"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "466,164 -> 470,164"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "464,109 -> 464,111 -> 463,111 -> 463,118 -> 473,118 -> 473,111 -> 468,111 -> 468,109"
        "461,87 -> 461,80 -> 461,87 -> 463,87 -> 463,80 -> 463,87 -> 465,87 -> 465,80 -> 465,87 -> 467,87 -> 467,86 -> 467,87"
        "478,33 -> 478,37 -> 471,37 -> 471,41 -> 483,41 -> 483,37 -> 482,37 -> 482,33"
        "460,96 -> 464,96"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "472,123 -> 472,124 -> 476,124"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "460,159 -> 460,154 -> 460,159 -> 462,159 -> 462,151 -> 462,159 -> 464,159 -> 464,152 -> 464,159"
        "461,87 -> 461,80 -> 461,87 -> 463,87 -> 463,80 -> 463,87 -> 465,87 -> 465,80 -> 465,87 -> 467,87 -> 467,86 -> 467,87"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "499,13 -> 499,16 -> 498,16 -> 498,19 -> 507,19 -> 507,16 -> 501,16 -> 501,13"
        "464,109 -> 464,111 -> 463,111 -> 463,118 -> 473,118 -> 473,111 -> 468,111 -> 468,109"
        "463,166 -> 467,166"
        "483,44 -> 483,48 -> 481,48 -> 481,55 -> 489,55 -> 489,48 -> 487,48 -> 487,44"
        "491,24 -> 496,24"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "488,26 -> 493,26"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "483,44 -> 483,48 -> 481,48 -> 481,55 -> 489,55 -> 489,48 -> 487,48 -> 487,44"
        "460,159 -> 460,154 -> 460,159 -> 462,159 -> 462,151 -> 462,159 -> 464,159 -> 464,152 -> 464,159"
        "499,13 -> 499,16 -> 498,16 -> 498,19 -> 507,19 -> 507,16 -> 501,16 -> 501,13"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "453,102 -> 458,102"
        "485,28 -> 490,28"
        "461,106 -> 466,106"
        "454,106 -> 459,106"
        "464,109 -> 464,111 -> 463,111 -> 463,118 -> 473,118 -> 473,111 -> 468,111 -> 468,109"
        "478,33 -> 478,37 -> 471,37 -> 471,41 -> 483,41 -> 483,37 -> 482,37 -> 482,33"
        "503,30 -> 508,30"
        "461,87 -> 461,80 -> 461,87 -> 463,87 -> 463,80 -> 463,87 -> 465,87 -> 465,80 -> 465,87 -> 467,87 -> 467,86 -> 467,87"
        "466,90 -> 470,90"
        "460,159 -> 460,154 -> 460,159 -> 462,159 -> 462,151 -> 462,159 -> 464,159 -> 464,152 -> 464,159"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "460,164 -> 464,164"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "496,30 -> 501,30"
        "461,87 -> 461,80 -> 461,87 -> 463,87 -> 463,80 -> 463,87 -> 465,87 -> 465,80 -> 465,87 -> 467,87 -> 467,86 -> 467,87"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "472,96 -> 476,96"
        "510,30 -> 515,30"
        "463,93 -> 467,93"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "475,99 -> 479,99"
        "466,168 -> 470,168"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "469,99 -> 473,99"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "464,109 -> 464,111 -> 463,111 -> 463,118 -> 473,118 -> 473,111 -> 468,111 -> 468,109"
        "499,13 -> 499,16 -> 498,16 -> 498,19 -> 507,19 -> 507,16 -> 501,16 -> 501,13"
        "483,44 -> 483,48 -> 481,48 -> 481,55 -> 489,55 -> 489,48 -> 487,48 -> 487,44"
        "495,26 -> 500,26"
        "460,159 -> 460,154 -> 460,159 -> 462,159 -> 462,151 -> 462,159 -> 464,159 -> 464,152 -> 464,159"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "499,28 -> 504,28"
        "483,44 -> 483,48 -> 481,48 -> 481,55 -> 489,55 -> 489,48 -> 487,48 -> 487,44"
        "461,87 -> 461,80 -> 461,87 -> 463,87 -> 463,80 -> 463,87 -> 465,87 -> 465,80 -> 465,87 -> 467,87 -> 467,86 -> 467,87"
        "483,44 -> 483,48 -> 481,48 -> 481,55 -> 489,55 -> 489,48 -> 487,48 -> 487,44"
        "461,87 -> 461,80 -> 461,87 -> 463,87 -> 463,80 -> 463,87 -> 465,87 -> 465,80 -> 465,87 -> 467,87 -> 467,86 -> 467,87"
        "469,93 -> 473,93"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "506,28 -> 511,28"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "469,166 -> 473,166"
        "460,159 -> 460,154 -> 460,159 -> 462,159 -> 462,151 -> 462,159 -> 464,159 -> 464,152 -> 464,159"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "468,137 -> 468,132 -> 468,137 -> 470,137 -> 470,130 -> 470,137 -> 472,137 -> 472,136 -> 472,137 -> 474,137 -> 474,127 -> 474,137 -> 476,137 -> 476,131 -> 476,137 -> 478,137 -> 478,130 -> 478,137 -> 480,137 -> 480,128 -> 480,137 -> 482,137 -> 482,128 -> 482,137 -> 484,137 -> 484,133 -> 484,137"
        "466,74 -> 466,71 -> 466,74 -> 468,74 -> 468,64 -> 468,74 -> 470,74 -> 470,65 -> 470,74 -> 472,74 -> 472,68 -> 472,74 -> 474,74 -> 474,68 -> 474,74 -> 476,74 -> 476,71 -> 476,74 -> 478,74 -> 478,65 -> 478,74 -> 480,74 -> 480,73 -> 480,74 -> 482,74 -> 482,64 -> 482,74"
        "461,87 -> 461,80 -> 461,87 -> 463,87 -> 463,80 -> 463,87 -> 465,87 -> 465,80 -> 465,87 -> 467,87 -> 467,86 -> 467,87"
    |]

let example =
    [|
        "498,4 -> 498,6 -> 496,6"
        "503,4 -> 502,4 -> 502,9 -> 494,9"
    |]

let parseInput (input : string array) : (int * int) list =
    input
    |> List.ofArray
    |> List.collect
        (fun line ->
            line.Split(" -> ", System.StringSplitOptions.RemoveEmptyEntries)
            |> List.ofArray
            |> List.map
                (fun point ->
                    let split = point.Split(",")
                    (int split.[0], int split.[1])
                )
            |> List.pairwise
            |> List.collect
                (fun ((x1, y1), (x2, y2)) ->
                    if (x1 = x2) then
                        let minY = min y1 y2
                        let maxY = max y1 y2
                        [ minY..maxY ]
                        |> List.map (fun y -> (x1, y))
                    else
                        let minX = min x1 x2
                        let maxX = max x1 x2
                        [ minX..maxX ]
                        |> List.map (fun x -> (x, y1))
                )
        )
    |> List.distinct

let dropSand 
    (highestY : int)
    (wallPointSet : Set<int * int>)
    =
    let pointIsEmpty =
        (fun x y ->
            wallPointSet |> Set.contains (x, y) |> not
        )

    let rec dropSand' x y =
        if (y > highestY) then
            None
        elif (wallPointSet |> Set.contains (500, 0)) then
            None
        elif (pointIsEmpty x (y + 1)) then
            dropSand' x (y + 1)
        elif (pointIsEmpty (x - 1) (y + 1)) then
            dropSand' (x - 1) (y + 1)
        elif (pointIsEmpty (x + 1) (y + 1)) then
            dropSand' (x + 1) (y + 1)
        else
            wallPointSet 
            |> Set.add (x, y) 
            |> Some
    dropSand' 500 0

let dropSandUntilEdgeHit wallPoints =
    let highestY = 
        wallPoints 
        |> List.maxBy 
            (fun (_, y) -> y) 
        |> snd

    wallPoints
    |> Set.ofList
    |> Seq.unfold (fun wallPointSet ->
        match dropSand highestY wallPointSet with
        | Some state' -> 
            Some (state', state')
        | None -> 
            None
    )
    |> Seq.last

[<Test>]
let Day14_Part1 () =
    let wallPoints =
        input
        |> parseInput

    let sandDropped =
        dropSandUntilEdgeHit wallPoints

    Assert.AreEqual(644, sandDropped.Count - wallPoints.Length)

[<Test>]
let Day14_Part2 () =    
    let wallPoints =
        let wallPoints' =
            input
            |> parseInput
            
        let highestY = 
            wallPoints'
            |> List.maxBy 
                (fun (_, y) -> y) 
            |> snd
            
        wallPoints'
        |> List.append 
            (
                [-10000..10000]
                |> List.map (fun x -> (x, highestY + 2))
            )

    let sandDropped =
        dropSandUntilEdgeHit wallPoints

    Assert.AreEqual(27324, sandDropped.Count - wallPoints.Length)
    