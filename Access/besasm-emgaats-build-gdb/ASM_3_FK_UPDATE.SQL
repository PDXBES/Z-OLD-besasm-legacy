USE ASM_MASTER_RPT_SDE
;
UPDATE l
SET l.us_node_id = n.node_id
FROM GIS.LINKS l INNER JOIN GIS.NODES n 
ON l.us_node_name_deprecated = n.node_name;

UPDATE l
SET l.ds_node_id = n.node_id
FROM GIS.LINKS l INNER JOIN GIS.NODES n 
ON l.ds_node_name_deprecated = n.node_name;

UPDATE l
SET l.pipe_shape = s.shape_id
FROM GIS.LINKS l INNER JOIN GIS.PIPE_SHAPES s
ON l.pipe_shape_deprecated = s.shape_name;

UPDATE psp
SET psp.shape_id = ps.shape_id
FROM GIS.PIPE_SHAPES_DATA psp INNER JOIN GIS.PIPE_SHAPES ps
ON psp.shape_deprecated = ps.shape_name;

UPDATE d
SET d.san_to_link_id = l.link_id
FROM GIS.DSCS d INNER JOIN GIS.LINKS l
ON d.san_to_mlinkid_deprecated = l.mlinkid_deprecated;

UPDATE d
SET d.storm_to_link_id = l.link_id
FROM GIS.DSCS d INNER JOIN GIS.LINKS l
ON d.storm_to_mlinkid_deprecated = l.mlinkid_deprecated;

UPDATE s
SET s.node_id_ex = n.node_id
FROM GIS.SSCS s INNER JOIN GIS.NODES n
ON s.node_name_ex_deprecated = n.node_name;

UPDATE s
SET s.node_id_fu= n.node_id
FROM GIS.SSCS s INNER JOIN GIS.NODES n
ON s.node_name_fu_deprecated = n.node_name;

UPDATE s
SET s.alt_trace_node_id= n.node_id
FROM GIS.SSCS s INNER JOIN GIS.NODES n
ON s.alt_trace_node_name_deprecated = n.node_name;

UPDATE r
SET r.dsc_id = d.dsc_id
FROM GIS.ROOF_TARGETS r INNER JOIN GIS.DSCS d
ON r.dscid_deprecated = d.dscid_deprecated;

UPDATE r
SET r.node_id = n.node_id
FROM GIS.ROOF_TARGETS r INNER JOIN GIS.NODES n
ON r.ngtoroof_deprecated = n.node_name;

UPDATE p
SET p.dsc_id = d.dsc_id
FROM GIS.PARKING_TARGETS p INNER JOIN GIS.DSCS d
ON p.dscid_deprecated = d.dscid_deprecated;

UPDATE p
SET p.node_id_dsc = n.node_id
FROM GIS.PARKING_TARGETS p INNER JOIN GIS.NODES n
ON p.nodeid_dsc_deprecated = n.node_name;

UPDATE p
SET p.node_id_ssc = n.node_id
FROM GIS.PARKING_TARGETS p INNER JOIN GIS.NODES n
ON p.nodeid_ssc_deprecated = n.node_name;

UPDATE p
SET p.node_id = n.node_id
FROM GIS.PARKING_TARGETS p INNER JOIN GIS.NODES n
ON p.nodeid_deprecated = n.node_name;

UPDATE s
SET s.node_id = n.node_id
FROM GIS.STREET_TARGETS s INNER JOIN GIS.NODES n
ON s.ngtostreet_deprecated = n.node_name;

UPDATE s
SET s.ssc_id = ssc.ssc_id
FROM GIS.STREET_TARGETS s INNER JOIN GIS.SSCS ssc
ON s.surfscid_deprecated = ssc.surfscid_deprecated;

UPDATE d
SET d.dsc_id = dsc.dsc_id
FROM GIS.IC_DRYWELLS d INNER JOIN GIS.DSCS dsc
ON d.dscid_deprecated = dsc.dscid_deprecated;

UPDATE d
SET d.dsc_id = dsc.dsc_id
FROM GIS.IC_DISCOS d INNER JOIN GIS.DSCS dsc
ON d.dscid_deprecated = dsc.dscid_deprecated;

UPDATE d
SET d.parcel_id = d.parcelid_deprecated, d.divide_id = d.divideid_deprecated
FROM GIS.IC_DRYWELLS d

UPDATE d
SET d.parcel_id = d.parcelid_deprecated, d.divide_id = d.divideid_deprecated
FROM GIS.IC_DISCOS d

UPDATE c
SET c.link_id = l.link_id
FROM GIS.CROSS_SECTIONS c INNER JOIN GIS.LINKS l
ON c.mlinkid_deprecated = l.mlinkid_deprecated;

UPDATE cd
SET cd.cross_section_id = c.cross_section_id
FROM GIS.CROSS_SECTIONS_DATA cd INNER JOIN GIS.CROSS_SECTIONS c
ON cd.mst_xsections_id_deprecated = c.mst_xsections_id_deprecated;

UPDATE sl
SET sl.node_id = n.node_id
FROM GIS.SPECIAL_LINKS sl INNER JOIN GIS.NODES n
ON sl.node_deprecated = n.node_name;

UPDATE sl
SET sl.to_link_id = l.link_id
FROM GIS.SPECIAL_LINKS sl INNER JOIN GIS.LINKS l
ON sl.tomlinkid_deprecated = l.mlinkid_deprecated;

UPDATE sld
SET sld.special_link_id = sl.special_link_id
FROM GIS.SPECIAL_LINKS_DATA sld INNER JOIN GIS.SPECIAL_LINKS sl
ON sld.slid_deprecated = sl.slid_deprecated;

UPDATE f
SET f.dsc_id = d.dsc_id
FROM GIS.FIXED_FLOOR_ELEVATIONS f INNER JOIN GIS.DSCS d
ON f.dscid_deprecated = d.dscid_deprecated;

UPDATE p
SET p.link_id = l.link_id
FROM GIS.PIPE_CONFLICTS p INNER JOIN GIS.LINKS l
ON p.mlinkid_deprecated = l.mlinkid_deprecated;

