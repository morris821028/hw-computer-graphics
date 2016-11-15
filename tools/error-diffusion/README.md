## Floay-Steinberg error diffusion dithering ##

### Compile ###

```bash
$ make
```


### Input Format ###

```bash
$ ./error-diffusion <quant_number>
```

```
<row_number R> <column_number C>
[m1,1, ... m1,C]
[m2,1, ... m2,C]
...
[mR,1, ... mR,C]
```

### Run ###

```bash
$ cat sample.in
$ ./error-diffusion 2 <sample.in
Case #1:
[6 6 4]
[2 2 4]
[4 8 6]
Case #2:
[2 4]
[6 2]
```

or

```bash
$ ./error-diffusion 2
2 2
3 5
7 1
Case #1:
[2 4]
[6 2]
```


