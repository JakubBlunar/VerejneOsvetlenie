select cislo, id_ulice, poradie from s_stlp ss 
where not exists(
 select 'x' from s_info si
  where si.datum > add_months(sysdate, -12) and
  ss.cislo = si.cislo
);