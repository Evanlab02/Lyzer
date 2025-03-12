# Valkey Migration

- Author: [Evanlab02](https://github.com/Evanlab02)
- Email: [evanlabuschagne70@gmail.com](mailto:evanlabuschagne70@gmail.com)
- Date: 2025-03-12
- Status: Review
- Planned: No
- Milestone: Not assigned

## Summary

Migrate from Redis to Valkey as our caching solution due to Redis licensing changes. This migration will ensure license compatibility for our project and prevent potential issues down the line, while maintaining all current caching functionality.

## Impact Assessment

- **Severity**: Medium
- **Scope**: Component
- **Risk Profile**: Medium risk if not addressed
- **Development Impact**: Low immediate impact, potential future licensing concerns
- **Area**: Backend

## Detailed Description

Lyzer currently uses Redis as a caching solution for F1 data, implemented through the `CacheService` using the StackExchange.Redis client library. Redis recently changed its licensing model, which could potentially affect our usage and compliance in the future.

Valkey is a Redis-compatible fork that maintains compatibility with the Redis API while offering a more permissive license. As the Redis project continues to evolve its licensing, migrating to Valkey would protect us from potential future restrictions or compliance issues.

Current implementation details:

- We use Redis 7.4.1-alpine3.20 in Docker containers
- Connection is managed via StackExchange.Redis 2.8.16 NuGet package
- Data is stored as serialized JSON strings with various TTLs (1-24 hours)
- Used throughout our services (RacesService, ResultsService, DriverService, ConstructorService) for caching API responses

## Proposed Resolution

**Replace Redis Docker images with Valkey images in all compose files:**

- Update `compose.yaml` and `compose.staging.yaml` to use Valkey instead of Redis
- Ensure proper image tagging and version selection


**Evaluate compatibility:**

- Confirm StackExchange.Redis works with Valkey (expected to work seamlessly)
- Document any configuration differences


**Testing:**

- Create comprehensive tests to verify caching functionality
- Validate all existing cache operations (Get, Add, Remove, exists)


**Documentation updates:**

- Update architecture docs to reflect the change from Redis to Valkey
- Document the migration process and any configuration changes

## Benefits of Resolution

- **License Compliance**: Protects the project from potential Redis license restrictions
- **Future-Proofing**: Reduces dependency on Redis' evolving license terms
- **Maintenance**: Valkey is actively maintained and Redis-compatible, ensuring long-term viability
- **No Functionality Change**: All current caching capabilities will be preserved
- **Knowledge Sharing**: Team familiarity with Redis will transfer to Valkey with minimal retraining

## Risks of Deferral

- **License Compliance Issues**: Future Redis license changes might conflict with our usage

## Acceptance Criteria

The migration will be considered successful when:

1. All Docker configurations use Valkey images instead of Redis
2. All existing caching functionality works identically with Valkey
3. All tests pass with the new implementation
4. Documentation is updated to reflect the change
