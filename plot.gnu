
if (!exists("s")) set terminal wxt size 1300,1000 enhanced font 'Lato Light,12' persist

s = 1

# Line width of the axes
set border linewidth 1.5
set key outside

set xrange [1512833674000:]
set yrange [14000:18000]

set multiplot layout 3,1
plot for [col=2:2] 'trades.dat' using 1:col with lines title columnheader linewidth 2
plot for [col=2:2] 'order_buys.dat' using 1:col with points title columnheader pt 7 pointsize .25
plot for [col=2:2] 'order_sells.dat' using 1:col with points title columnheader pt 7 pointsize .25
pause .5
reread
    
pause -1 'press Ctrl-D to exit
