#include <bits/stdc++.h>
using namespace std;

const float eps = 1e-6;

struct Tri {
	float fc[3], bc[3];
	float vxyz[3][3];
	float nxyz[3][3];
};

struct Pt {
    float x, y, z;
    Pt(float x = 0.f, float y = 0.f, float z = 0.f):
    	x(x), y(y), z(z) {}	
    bool operator==(const Pt &a) const {
    	return fabs(x - a.x) < eps && fabs(y - a.y) < eps && fabs(z - a.z) < eps;
    }
    bool operator!=(const Pt &a) const {
    	return !(a == *this);
    }
    bool operator<(const Pt &a) const {
        if (fabs(x - a.x) > eps)
            return x < a.x;
        if (fabs(y - a.y) > eps)
            return y < a.y;
        if (fabs(z - a.z) > eps)
            return z < a.z;
        return false;
    }
};

void readLine(string line, float f[]) {
	stringstream sin(line);
	for (int i = 0; i < 6; i++) {
		assert(sin >> f[i]);
	}
}
int main(int argc, char *argv[]) {
	if (argc == 2) {
		if (!strcmp("--help", argv[1])) {
			fprintf(stderr, "\t\t--format\t\tModel File Format\n\t\t\t\t"
				"--format SIMPLE / --format COLOR\n");
			exit(0);
		}
		exit(1);
	}
	if (argc < 5) {
		fprintf(stderr, "./tri2json -i <filename.tri> -o <jsonname.json> [--format]\n");
		fprintf(stderr, "./tri2json --help\n");
		exit(1);
	}

	string ifileName = "", ofileName = "", fileFormat = "COLOR";

	for (int i = 1; i < argc; i++) {
		if (!strcmp(argv[i], "-i") && i+1 < argc) {
			ifileName = argv[i+1], i++;
		} else if (!strcmp(argv[i], "-o") && i+1 < argc) {
			ofileName = argv[i+1], i++;
		} else if (!strcmp(argv[i], "--format") && i+1 < argc) {
			assert((!strcmp(argv[i+1], "COLOR") || !strcmp(argv[i+1], "SIMPLE")) && "Please check file format");
			fileFormat = argv[i+1], i++;
		}
	}
	assert(ifileName.length() && "Please give input file name");
	assert(ofileName.length() && "Please give output file name");

	ifstream fin(ifileName);
	ofstream fout(ofileName);

	string objName;
	vector<Tri> A;
	while (getline(fin, objName)) {
		string line;
		float f[6];
		Tri t;	

		if (fileFormat == "COLOR") {
			assert(getline(fin, line));
			readLine(line, f);

			for (int i = 0; i < 3; i++)
				t.fc[i] = f[i], t.bc[i] = f[i+3];
		}

		for (int i = 0; i < 3; i++) {
			assert(getline(fin, line));
			readLine(line, f);
			for (int j = 0; j < 3; j++) {
				t.vxyz[i][j] = f[j];
				t.nxyz[i][j] = f[j+3];
			}
			float sc = 5.f;
			t.vxyz[i][0] -= 400.0f * 10;
			t.vxyz[i][0] /= sc;
			t.vxyz[i][0] -= 220.0f;
			t.vxyz[i][1] /= sc;
			t.vxyz[i][1] -= 750.0f;
			t.vxyz[i][2] -= 500.0f;
			t.vxyz[i][2] /= sc;
		}
		A.push_back(t);
	}
	
	map< pair<Pt, Pt> , int> V;
	for (int i = 0; i < A.size(); i++) {
		Tri t = A[i];
		for (int j = 0; j < 3; j++) {
			Pt a, b;
			a = Pt(t.vxyz[j][0], t.vxyz[j][1], t.vxyz[j][2]);
			b = Pt(t.nxyz[j][0], t.nxyz[j][1], t.nxyz[j][2]);
			V[make_pair(a, b)] = 0;
		}
	}

	{
		int label = 0;
		for (auto &e : V)
			e.second = label++;
	}

	fout << "{" << endl;

	fout << "\t\"vertexPositions\" : [";
	for (map< pair<Pt, Pt>, int>::iterator it = V.begin(); it != V.end(); it++) {
		if (it != V.begin())
			fout << ",";
		fout << it->first.first.x << "," << it->first.first.y << "," << it->first.first.z;
	}
	fout << "]," << endl;

	fout << "\t\"vertexNormals\" : [";
	for (map< pair<Pt, Pt>, int>::iterator it = V.begin(); it != V.end(); it++) {
		if (it != V.begin())
			fout << ",";
		fout << it->first.second.x << "," << it->first.second.y << "," << it->first.second.z;
	}
	fout << "]," << endl;
	
	fout << "\t\"vertexTextureCoords\" : [";
	for (map< pair<Pt, Pt>, int>::iterator it = V.begin(); it != V.end(); it++) {
		if (it != V.begin())
			fout << ",";
		fout << 0.5 << "," << 0.5 << "," << 0.5;
	}
	fout << "]," << endl;

	fout << "\t\"indices\" : [";
	for (int i = 0; i < A.size(); i++) {
		if (i != 0)
			fout << ",";
		Tri t = A[i];
		for (int j = 0; j < 3; j++) {
			Pt a, b;
			a = Pt(t.vxyz[j][0], t.vxyz[j][1], t.vxyz[j][2]);
			b = Pt(t.nxyz[j][0], t.nxyz[j][1], t.nxyz[j][2]);
			if (j != 0)
				fout << ",";
			fout << V[make_pair(a, b)];
		}
	}
	fout << "]" << endl;

	fout << "}" << endl;
	printf("%d\n", V.size());
	return 0;
}

