select id_ulice, nazov, mesto
from s_ulica u
group by id_ulice, nazov, mesto
having
(
select count(id_lampy) 
from s_stlp ss
left join s_lampa_na_stlpe ls using(cislo)
where ls.stav = 'N' and ls.datum_demontaze is null
and ss.id_ulice = u.id_ulice
group by ss.id_ulice ) / (
select count(*) 
from s_stlp ss
left join s_lampa_na_stlpe ls using(cislo)
where ls.datum_demontaze is null
and ss.id_ulice = u.id_ulice
group by ss.id_ulice) > 0.15;