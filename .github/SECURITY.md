# Security Policy

## Supported Versions

StreamMaster follows a continuous deployment model. Only the latest version receives security updates.

| Version | Supported          |
| ------- | ------------------ |
| Latest  | :white_check_mark: |
| < Latest| :x:                |

## Reporting a Vulnerability

We take security vulnerabilities seriously, especially given that StreamMaster handles media streams and integrates with various server platforms.

### How to Report

1. **DO NOT** create a public GitHub issue for security vulnerabilities
2. Please [open a private security advisory](https://github.com/carlreid/StreamMaster/security/advisories/new) through GitHub's Security tab
3. Include:
   - Description of the vulnerability
   - Steps to reproduce
   - Potential impact
   - Affected versions
   - Possible fixes (if known)

### What to Expect

- Credit for responsible disclosure (if desired)

## Security Best Practices

When deploying StreamMaster:

1. Always use HTTPS for M3U and EPG URL imports
2. Regularly update to the latest version
3. Use strong authentication for admin access

## Scope

Security concerns include but are not limited to:
- Authentication and authorization
- Stream access control
- Data handling and storage
- API security
- Dependencies and third-party components
