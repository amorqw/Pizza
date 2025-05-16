-- Insert test services
INSERT INTO Service (id_service, service_name, cost) VALUES
    ('11111111-1111-1111-1111-111111111111', 'Oil Change', 50),
    ('22222222-2222-2222-2222-222222222222', 'Brake Service', 100),
    ('33333333-3333-3333-3333-333333333333', 'Engine Repair', 200),
    ('44444444-4444-4444-4444-444444444444', 'Tire Replacement', 80),
    ('55555555-5555-5555-5555-555555555555', 'Battery Replacement', 120);

-- Insert test roles
INSERT INTO Role (id_role, role_name) VALUES
    ('aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa', 'Admin'),
    ('bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb', 'User'); 