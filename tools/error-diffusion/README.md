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
```

or

```bash
$ ./error-diffusion 2
2 2
3 5
7 1
```


