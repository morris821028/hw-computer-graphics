all:
	g++ -std=c++11 -O2 tri2json.cpp -o tri2json
test:
	./tri2json -i Pot.tri -o Pot.json --format SIMPLE
move:
	mv Pot.json ~/Desktop/github/hw-computer-graphics/hw1-shading-cmp/WebGL/asset/
clean:
	rm tri2json
