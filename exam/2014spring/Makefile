TEX_FILE = $(wildcard *.tex)
SPELL_CHECK_TARGETS=$(TEX_FILE)
LATEX_ENGINE=xelatex

all: main.pdf
.PHONY: all clean spellcheck

main.pdf: main.tex
	$(LATEX_ENGINE) main.tex
	$(LATEX_ENGINE) main.tex

spellcheck: $(TEX_FILE)
	@for f in $(TEX_FILE); do \
		aspell --mode=tex --personal=./.aspell.en.pws check $$f; \
	done

clean:
	rm -f *.aux *.log *.nav *.out *.pdf *.snm *.toc *.vrb
