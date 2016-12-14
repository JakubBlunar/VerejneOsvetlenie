select id_typu, svietivost from s_lampa_na_stlpe
join s_lampa using(id_typu)
where id_lampy = (
  select id_lampy from s_obsluha_lampy
  join s_servis using(id_sluzby)
  group by id_lampy
  having count(id_lampy) = (
    select max(count(id_lampy)) from s_obsluha_lampy
    join s_servis using(id_sluzby)
    group by id_lampy 
  )
);