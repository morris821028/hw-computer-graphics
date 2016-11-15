#include <bits/stdc++.h>
using namespace std;

int main(int argc, char *argv[]) {
	assert(argc == 2 && "./error-diffusion <quant_number>");
	const double quant_number = (double) atoi(argv[1]);
	int n, m;
	int cases = 0;
	while (scanf("%d %d", &n, &m) == 2) {
		double mtx[16][16];
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < m; j++) {
				scanf("%lf", &mtx[i][j]);
			}
		}
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < m; j++) {
				double u = mtx[i][j];
				double v = floor(mtx[i][j] / quant_number) * quant_number;
				mtx[i][j] = v;
				double quant_error = u - v;
				if (i+1 < n)
					mtx[i+1][j] += quant_error * 3 / 8.f;
				if (j+1 < m)
					mtx[i][j+1] += quant_error * 3 / 8.f;
				if (i+1 < n && j+1 < m)
					mtx[i+1][j+1] += quant_error * 2 / 8.f;
			}
		}
		
		printf("Case #%d:\n", ++cases);
		for (int i = 0; i < n; i++) {
			printf("[");
			for (int j = 0; j < m; j++)
				printf("%g%c", mtx[i][j], " ]"[j==m-1]);
			puts("");
		}
	}
	return 0;
}

/*
3 3
7 7 5
3 1 3
5 8 7
*/

