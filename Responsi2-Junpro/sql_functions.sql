
-- 1. FUNCTION: GET ALL DEVELOPERS DENGAN JOIN PROYEK
CREATE OR REPLACE FUNCTION get_all_developers()
RETURNS TABLE(
    id_dev INT,
    nama_dev VARCHAR(50),
    nama_proyek VARCHAR(100),
    status_kontrak VARCHAR(20),
    fitur_selesai INT,
    jumlah_bug INT
) AS $$
BEGIN
    RETURN QUERY
    SELECT 
        d.id_dev,
        d.nama_dev,
        p.nama_proyek,
        d.status_kontrak,
        d.fitur_selesai,
        d.jumlah_bug
    FROM developer d
    JOIN proyek p ON d.id_proyek = p.id_proyek
    ORDER BY d.id_dev;
END;
$$ LANGUAGE plpgsql;

-- 2. FUNCTION: GET DEVELOPER BY ID
CREATE OR REPLACE FUNCTION get_developer_by_id(p_id_dev INT)
RETURNS TABLE(
    id_dev INT,
    nama_dev VARCHAR(50),
    nama_proyek VARCHAR(100),
    status_kontrak VARCHAR(20),
    fitur_selesai INT,
    jumlah_bug INT
) AS $$
BEGIN
    RETURN QUERY
    SELECT 
        d.id_dev,
        d.nama_dev,
        p.nama_proyek,
        d.status_kontrak,
        d.fitur_selesai,
        d.jumlah_bug
    FROM developer d
    JOIN proyek p ON d.id_proyek = p.id_proyek
    WHERE d.id_dev = p_id_dev;
END;
$$ LANGUAGE plpgsql;

-- 3. FUNCTION: GET ALL PROYEK
CREATE OR REPLACE FUNCTION get_all_proyek()
RETURNS TABLE(
    id_proyek INT,
    nama_proyek VARCHAR(100),
    budget NUMERIC(15, 2)
) AS $$
BEGIN
    RETURN QUERY
    SELECT p.id_proyek, p.nama_proyek, p.budget
    FROM proyek p
    ORDER BY p.nama_proyek;
END;
$$ LANGUAGE plpgsql;

-- 4. FUNCTION: GET ID PROYEK BY NAMA
CREATE OR REPLACE FUNCTION get_id_proyek_by_nama(p_nama_proyek VARCHAR(100))
RETURNS INT AS $$
DECLARE
    v_id_proyek INT;
BEGIN
    SELECT p.id_proyek INTO v_id_proyek
    FROM proyek p
    WHERE p.nama_proyek = p_nama_proyek;
    
    RETURN v_id_proyek;
END;
$$ LANGUAGE plpgsql;

-- 5. FUNCTION: GET BUDGET PROYEK
CREATE OR REPLACE FUNCTION get_budget_proyek(p_nama_proyek VARCHAR(100))
RETURNS NUMERIC(15, 2) AS $$
DECLARE
    v_budget NUMERIC(15, 2);
BEGIN
    SELECT p.budget INTO v_budget
    FROM proyek p
    WHERE p.nama_proyek = p_nama_proyek;
    
    RETURN COALESCE(v_budget, 0);
END;
$$ LANGUAGE plpgsql;

-- 6. FUNCTION: GET TOTAL GAJI PROYEK (Sum semua gaji developer dalam proyek)
CREATE OR REPLACE FUNCTION get_total_pengeluaran_proyek(p_nama_proyek VARCHAR(100))
RETURNS TABLE(
    nama_dev VARCHAR(50),
    status_kontrak VARCHAR(20),
    fitur_selesai INT,
    jumlah_bug INT
) AS $$
BEGIN
    RETURN QUERY
    SELECT 
        d.nama_dev,
        d.status_kontrak,
        d.fitur_selesai,
        d.jumlah_bug
    FROM developer d
    JOIN proyek p ON d.id_proyek = p.id_proyek
    WHERE p.nama_proyek = p_nama_proyek;
END;
$$ LANGUAGE plpgsql;

-- 7. FUNCTION: INSERT DEVELOPER
CREATE OR REPLACE FUNCTION insert_developer(
    p_nama_dev VARCHAR(50),
    p_status_kontrak VARCHAR(20),
    p_fitur_selesai INT,
    p_jumlah_bug INT,
    p_nama_proyek VARCHAR(100)
)
RETURNS INT AS $$
DECLARE
    v_id_proyek INT;
    v_new_id INT;
BEGIN
    -- Get id_proyek dari nama_proyek
    SELECT p.id_proyek INTO v_id_proyek
    FROM proyek p
    WHERE p.nama_proyek = p_nama_proyek;
    
    IF v_id_proyek IS NULL THEN
        RAISE EXCEPTION 'Proyek tidak ditemukan: %', p_nama_proyek;
    END IF;
    
    -- Insert developer
    INSERT INTO developer (nama_dev, status_kontrak, fitur_selesai, jumlah_bug, id_proyek)
    VALUES (p_nama_dev, p_status_kontrak, p_fitur_selesai, p_jumlah_bug, v_id_proyek)
    RETURNING developer.id_dev INTO v_new_id;
    
    RETURN v_new_id;
END;
$$ LANGUAGE plpgsql;

-- 8. FUNCTION: UPDATE DEVELOPER
CREATE OR REPLACE FUNCTION update_developer(
    p_id_dev INT,
    p_nama_dev VARCHAR(50),
    p_status_kontrak VARCHAR(20),
    p_fitur_selesai INT,
    p_jumlah_bug INT,
    p_nama_proyek VARCHAR(100)
)
RETURNS BOOLEAN AS $$
DECLARE
    v_id_proyek INT;
    v_rows_affected INT;
BEGIN
    -- Get id_proyek dari nama_proyek
    SELECT p.id_proyek INTO v_id_proyek
    FROM proyek p
    WHERE p.nama_proyek = p_nama_proyek;
    
    IF v_id_proyek IS NULL THEN
        RAISE EXCEPTION 'Proyek tidak ditemukan: %', p_nama_proyek;
    END IF;
    
    -- Update developer
    UPDATE developer
    SET nama_dev = p_nama_dev,
        status_kontrak = p_status_kontrak,
        fitur_selesai = p_fitur_selesai,
        jumlah_bug = p_jumlah_bug,
        id_proyek = v_id_proyek
    WHERE id_dev = p_id_dev;
    
    GET DIAGNOSTICS v_rows_affected = ROW_COUNT;
    
    RETURN v_rows_affected > 0;
END;
$$ LANGUAGE plpgsql;

-- 9. FUNCTION: DELETE DEVELOPER
CREATE OR REPLACE FUNCTION delete_developer(p_id_dev INT)
RETURNS BOOLEAN AS $$
DECLARE
    v_rows_affected INT;
BEGIN
    DELETE FROM developer WHERE id_dev = p_id_dev;
    
    GET DIAGNOSTICS v_rows_affected = ROW_COUNT;
    
    RETURN v_rows_affected > 0;
END;
$$ LANGUAGE plpgsql;

-- 10. FUNCTION: CEK BUDGET TERSEDIA
CREATE OR REPLACE FUNCTION cek_budget_tersedia(p_nama_proyek VARCHAR(100))
RETURNS NUMERIC(15, 2) AS $$
DECLARE
    v_budget NUMERIC(15, 2);
BEGIN
    SELECT p.budget INTO v_budget
    FROM proyek p
    WHERE p.nama_proyek = p_nama_proyek;
    
    RETURN COALESCE(v_budget, 0);
END;
$$ LANGUAGE plpgsql;
