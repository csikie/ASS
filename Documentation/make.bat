@ECHO OFF
pdflatex thesis.tex
bibtex thesis
makeindex -s nomencl.ist -t thesis.nlg -o thesis.nls thesis.nlo
pdflatex thesis.tex
pdflatex thesis.tex