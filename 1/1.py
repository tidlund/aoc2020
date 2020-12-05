#!/usr/bin/env python3

div_set = set([])

with open("input.txt") as file_input:
    for line in file_input:
        nr = int(line)
        div_set.add(nr)
        if (2020-nr) in div_set:
            print(nr*(2020-nr))

for n in div_set:
    for m in div_set:
        if (2020-n-m) in div_set:
            print(n*m*(2020-n-m))
