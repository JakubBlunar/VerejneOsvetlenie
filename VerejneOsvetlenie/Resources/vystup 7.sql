select cislo, svietivost from(

select cislo, sum(svietivost_lampy(id_lampy)) svietivost , row_number() over(order by sum(svietivost_lampy(id_lampy)) asc) dr
from s_stlp
join s_lampa_na_stlpe using(cislo)
where datum_demontaze is null and stav = 'S'
group by cislo
) tb
where (dr / (select count(*) from s_stlp)* 100 ) <= 10;
