select cislo from (  
    select ss.cislo, count(nest.id) poc
    from s_stlp ss, table(select doplnky from s_stlp c where c.cislo = ss.cislo) nest
    where nest.typ_doplnku IN('Z', 'O')
    and extract( year from nest.datum_instalacie) = extract( year from add_months(sysdate, -12 ))
    group by ss.cislo
) where poc > 1;


